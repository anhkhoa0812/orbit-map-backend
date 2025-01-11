using System.Linq.Expressions;
using System.Security.Authentication;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrbitMap.API.Payload.Request.User;
using OrbitMap.API.Payload.Response.User;
using OrbitMap.API.Services.Interface;
using OrbitMap.API.Utils;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;
using LoginRequest = OrbitMap.API.Payload.Request.User.LoginRequest;
using RegisterRequest = OrbitMap.API.Payload.Request.User.RegisterRequest;

namespace OrbitMap.API.Services.Implement;

public class UserService : BaseService<UserService>, IUserService
{
    private readonly IRedisService _redisService;
    private readonly IUploadService _uploadService;

    public UserService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, IRedisService redisService, IUploadService uploadService) : base(
        unitOfWork, logger, mapper, httpContextAccessor)
    {
        _redisService = redisService;
        _uploadService = uploadService;
    }

    public async Task<LoginResponse> Login(LoginRequest loginRequest)
    {
        Expression<Func<User, bool>> searchFilter = p =>
            p.Username.Equals(loginRequest.Username) &&
            p.PasswordHash.Equals(PasswordUtil.HashPassword(loginRequest.Password));
        var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
            predicate: searchFilter,
            include: x => x.Include(x => x.Role)
        );
        if (user == null) throw new BadHttpRequestException("Invalid username or password");

        var guidClaim = new Tuple<string, Guid>("userId", user.Id);
        var result = _mapper.Map<LoginResponse>(user);
        var token = JwtUtil.GenerateJwtToken(user, guidClaim);
        if (user.Role.Name.Equals(ERoleEnum.Member.ToString()))
        {
            var member = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
                predicate: x => x.Id.Equals(user.Id)
            );
            result.IsPremium = member.IsPremium;
            result.Birthday = member.Birthday;
        }

        result.Token = token;
        return result;
    }

    public async Task<LoginResponse> Register(RegisterRequest registerRequest)
    {
        var userList = await _unitOfWork.GetRepository<Member>().GetListAsync();
        if (userList.Any(x => x.Username.Equals(registerRequest.Username)))
            throw new BadHttpRequestException("Username is already taken");
        if (userList.Any(x => x.PhoneNumber.Equals(registerRequest.PhoneNumber)))
            throw new BadHttpRequestException("Phone number is already taken");

        var user = _mapper.Map<Member>(registerRequest);

        var key = registerRequest.PhoneNumber;
        var existingOtp = await _redisService.GetStringAsync(key);

        if (string.IsNullOrEmpty(existingOtp))
            throw new BadHttpRequestException("Can not find OTP code");
        if (!existingOtp.Equals(registerRequest.Otp))
            throw new BadHttpRequestException("Invalid OTP code");
        user.PasswordHash = PasswordUtil.HashPassword(registerRequest.Password);
        var role = await _unitOfWork.GetRepository<Role>().SingleOrDefaultAsync(
            predicate: x => x.Name.Equals(ERoleEnum.Member.ToString())
        );
        user.RoleId = role.Id;
        user.Id = Guid.NewGuid();
        user.IsPremium = false;

        await _unitOfWork.GetRepository<Member>().InsertAsync(user);

        var isSuccess = await _unitOfWork.CommitAsync() > 0;

        if (!isSuccess) throw new Exception("Register failed");

        var guidClaim = new Tuple<string, Guid>("userId", user.Id);
        var result = _mapper.Map<LoginResponse>(user);
        var userFromDb = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: u => u.Username == user.Username,
            include: u => u.Include(u => u.Role)
        );
        var token = JwtUtil.GenerateJwtToken(userFromDb, guidClaim);
        result.Token = token;
        return result;
    }

    public async Task<UserDto> UpdateProfile(string username, UpdateUserRequest updateUserRequest)
    {
        if (string.IsNullOrEmpty(username))
            throw new AuthenticationException("Unauthorized");
        var userList = await _unitOfWork.GetRepository<Member>().GetListAsync();
        if (!string.IsNullOrEmpty(updateUserRequest.Username) &&
            userList.Any(x => x.Username.Equals(updateUserRequest.Username)))
            throw new BadHttpRequestException("Username is already taken");
        var currentUser = userList.SingleOrDefault(x => x.Username.Equals(username));
        if (currentUser == null) throw new BadHttpRequestException("User not found");
        var updatedUser = _mapper.Map(updateUserRequest, currentUser);
        if (!string.IsNullOrEmpty(updateUserRequest.ImageBase64))
        {
            var imageUrl = await _uploadService.UploadImageAsync(updateUserRequest.ImageBase64);
            updatedUser.AvatarUrl = imageUrl.SecureUrl.ToString();
        }

        _unitOfWork.GetRepository<Member>().UpdateAsync(updatedUser);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<UserDto>(updatedUser);
    }
}