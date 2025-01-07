using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public UserService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, IRedisService redisService) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
        _redisService = redisService;
    }

    public async Task<LoginResponse> Login(LoginRequest loginRequest)
    {
        Expression<Func<User, bool>> searchFilter = p =>
            p.Username.Equals(loginRequest.Username) &&
            p.PasswordHash.Equals(PasswordUtil.HashPassword(loginRequest.Password));
        User user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
            predicate: searchFilter,
            include: x => x.Include(x => x.Role)
        );
        if (user == null) throw new BadHttpRequestException("Invalid username or password");
        
        ERoleEnum roleEnum = EnumUtil.ParseEnum<ERoleEnum>(user.Role.Name);
        var guidClaim = new Tuple<string, Guid>("userId", user.Id);
        var result = _mapper.Map<LoginResponse>(user);
        var token = JwtUtil.GenerateJwtToken(user, guidClaim);
        result.Token = token;
        return result;
    }

    public async Task<LoginResponse> Register(RegisterRequest registerRequest)
    {
        var userList = await _unitOfWork.GetRepository<User>().GetListAsync();
        if(userList.Any(x => x.Username.Equals(registerRequest.Username)))
            throw new BadHttpRequestException("Username is already taken");
        if (userList.Any(x => x.PhoneNumber.Equals(registerRequest.PhoneNumber)))
            throw new BadHttpRequestException("Phone number is already taken");
        
        var user = _mapper.Map<User>(registerRequest);

        var key = registerRequest.PhoneNumber;
        var existingOtp = await _redisService.GetStringAsync(key);
        
        if(string.IsNullOrEmpty(existingOtp)) 
            throw new BadHttpRequestException("Can not find OTP code");
        if(!existingOtp.Equals(registerRequest.Otp)) 
            throw new BadHttpRequestException("Invalid OTP code");
        user.PasswordHash = PasswordUtil.HashPassword(registerRequest.Password);
        var role = await _unitOfWork.GetRepository<Role>().SingleOrDefaultAsync(
            predicate: x => x.Name.Equals(ERoleEnum.Member.ToString())
        );
        user.RoleId = role.Id;
        user.Id = Guid.NewGuid();
        user.IsPremium = false;
        
        await _unitOfWork.GetRepository<User>().InsertAsync(user);

        var isSuccess = await _unitOfWork.CommitAsync() > 0;
        
        if(!isSuccess) throw new Exception("Register failed");
        
        var response = _mapper.Map<LoginResponse>(user);
        var guidClaim = new Tuple<string, Guid>("userId", user.Id);
        var token = JwtUtil.GenerateJwtToken(user, guidClaim);
        response.Token = token;
        return response;
    }
}