using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrbitMap.API.Payload.Request.User;
using OrbitMap.API.Payload.Response.Business;
using OrbitMap.API.Payload.Response.Hotel;
using OrbitMap.API.Payload.Response.Restaurant;
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
    private readonly IFoodyService _foodyService;
    private readonly IOverseaService _overseaService;

    public BusinessService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IRedisService redisService,
        IFoodyService foodyService,
        IOverseaService overseaService) : base(unitOfWork, logger, mapper,
        httpContextAccessor)
    {
        _redisService = redisService;
        _foodyService = foodyService;
        _overseaService = overseaService;
    }

    public async Task<CreateBusinessResponse> CreateBusinessAsync(CreateBusinessRequest request)
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
        var result = _mapper.Map<CreateBusinessResponse>(user);
        var userFromDb = await _unitOfWork.GetRepository<Business>().SingleOrDefaultAsync(
            predicate: u => u.Username == user.Username,
            include: u => u.Include(u => u.Role)
        );
        var token = JwtUtil.GenerateJwtToken(userFromDb, guidClaim);
        result.Token = token;
        return result;
    }

    public async Task<List<RestaurantItemDto>> GetNearestRestaurant(double latitude, double longitude, string location)
    {
        var response = new List<RestaurantItemDto>();
        var businessRestaurant = await _unitOfWork.GetRepository<Business>().GetListAsync(
            predicate: x => x.Location!.Name.Equals(location)
                            && x.BusinessService != null
                            && x.BusinessType == EBusinessType.Restaurant,
            include: x => x.Include(x => x.Location)
        );
        if (businessRestaurant.Any())
        {
            foreach (var business in businessRestaurant)
            {
                var restaurant = new RestaurantItemDto
                {
                    Name = business.DisplayName,
                    Address = business.Address,
                    Avatar = business.AvatarUrl!
                };
                response.Add(restaurant);
            }
        }

        var foodyRestaurant = await _foodyService.GetNearestRestaurant(latitude, longitude);
        response.AddRange(foodyRestaurant);

        return response;
    }

    public async Task<List<HotelResponse>> GetNearestHotel(double latitude, double longitude, string location)
    {
        var response = new List<HotelResponse>();
        var businessRestaurant = await _unitOfWork.GetRepository<Business>().GetListAsync(
            predicate: x => x.Location!.Name.Equals(location)
                            && x.BusinessService != null
                            && x.BusinessType == EBusinessType.Hotel,
            include: x => x.Include(x => x.Location)
        );
        if (businessRestaurant.Any())
        {
            foreach (var business in businessRestaurant)
            {
                var hotel = new HotelResponse
                {
                    Name = business.DisplayName,
                    Address = business.Address,
                    Avatar = business.AvatarUrl!
                };
                response.Add(hotel);
            }
        }

        var overseaHotel = await _overseaService.GetNearestHotelFromOversea(latitude, longitude);
        if (overseaHotel.Any())
        {
            response.AddRange(overseaHotel);
        }

        return response;
    }

    public async Task<List<BusinessResponse>> GetBusinessesAsync()
    {
        var businesses = await _unitOfWork.GetRepository<Business>().GetListAsync(
            predicate: x => x.BusinessService != null
        );
        if (businesses.Any())
        {
            var response = _mapper.Map<List<BusinessResponse>>(businesses);
            return response;
        }

        return new List<BusinessResponse>();
    }
}