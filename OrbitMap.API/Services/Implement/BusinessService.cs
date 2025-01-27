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

namespace OrbitMap.API.Services.Implement;

public class BusinessService : BaseService<BusinessService>, IBusinessService
{
    private readonly IRedisService _redisService;

    public BusinessService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, IRedisService redisService) : base(unitOfWork, logger, mapper,
        httpContextAccessor)
    {
        _redisService = redisService;
    }

    public async Task<BusinessResponse> CreateBusinessAsync(CreateBusinessRequest request)
    {
        var userList = await _unitOfWork.GetRepository<Business>().GetListAsync();
        if (userList.Any(x => x.Username.Equals(request.Username)))
            throw new BadHttpRequestException("Username is already taken");
        if (userList.Any(x => x.PhoneNumber.Equals(request.PhoneNumber)))
            throw new BadHttpRequestException("Phone number is already taken");

        var user = _mapper.Map<Business>(request);

        var key = request.PhoneNumber;
        var existingOtp = await _redisService.GetStringAsync(key);

        if (string.IsNullOrEmpty(existingOtp))
            throw new BadHttpRequestException("Can not find OTP code");
        if (!existingOtp.Equals(request.Otp))
            throw new BadHttpRequestException("Invalid OTP code");
        user.PasswordHash = PasswordUtil.HashPassword(request.Password);
        var role = await _unitOfWork.GetRepository<Role>().SingleOrDefaultAsync(
            predicate: x => x.Name.Equals(ERoleEnum.Business.ToString())
        );
        user.RoleId = role.Id;
        user.Id = Guid.NewGuid();
        await _unitOfWork.GetRepository<Business>().InsertAsync(user);

        var isSuccess = await _unitOfWork.CommitAsync() > 0;

        if (!isSuccess) throw new Exception("Register failed");

        var guidClaim = new Tuple<string, Guid>("userId", user.Id);
        var result = _mapper.Map<BusinessResponse>(user);
        var userFromDb = await _unitOfWork.GetRepository<Business>().SingleOrDefaultAsync(
            predicate: u => u.Username == user.Username,
            include: u => u.Include(u => u.Role)
        );
        var token = JwtUtil.GenerateJwtToken(userFromDb, guidClaim);
        result.Token = token;
        return result;
    }
}