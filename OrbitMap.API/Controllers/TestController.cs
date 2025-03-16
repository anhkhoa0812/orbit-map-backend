using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OrbitMap.API.Helper;
using OrbitMap.API.Payload.Response.Location;
using OrbitMap.API.Payload.Response.User;
using OrbitMap.API.Services.Interface;
using OrbitMap.API.SignalR;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Member = AutoMapper.Execution.Member;

namespace OrbitMap.API.Controllers;

[ApiController]
[Route("/api/v1/test")]
public class TestController : BaseController<TestController>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork<OrbitMapContext> _unitOfWork;
    private readonly IOverseaService _overseaService;
    private readonly IVietMapService _vietMapService;
    private readonly IRedisService _redisService;

    public TestController(ILogger logger, IMapper mapper,
        IUnitOfWork<OrbitMapContext> unitOfWork, IOverseaService overseaService, IVietMapService vietMapService,
        IRedisService redisService) :
        base(logger)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _overseaService = overseaService;
        _vietMapService = vietMapService;
        _redisService = redisService;
    }

    // [HttpGet]
    // public async Task<IActionResult> GeneratePassport()
    // {
    //     var result = await _craftMyPdfService.GeneratePassport(User.GetUsername());
    //     return Ok(result);
    // }


    private async Task<List<UserDto>> GetUsersOnlineAsync(string currentUsername)
    {
        // Lấy thông tin người dùng hiện tại
        var currentUser = await _unitOfWork.GetRepository<Domain.Entities.Member>().SingleOrDefaultAsync(
            predicate: u => u.Username == currentUsername
        );

        if (currentUser == null) return new List<UserDto>();

        // Lấy danh sách bạn bè của người dùng hiện tại
        var friends = await _unitOfWork.GetRepository<Friendship>().GetListAsync(
            predicate: f => (f.RequesterId == currentUser.Id || f.AddresseeId == currentUser.Id) &&
                            f.Status == EFriendshipStatus.Accepted,
            include: f => f.Include(f => f.Requester).Include(f => f.Addressee)
        );
        var friendEntities = friends.Select(f => f.RequesterId == currentUser.Id ? f.Addressee : f.Requester).ToList();
        var result = _mapper.Map<List<UserDto>>(friendEntities);
        return result;
    }

    [HttpGet("/api/v1/test/oversea")]
    public async Task<IActionResult> GetNearestHotelFromOversea([FromQuery] double lat, [FromQuery] double lng)
    {
        var result = await _overseaService.GetNearestHotelFromOversea(lat, lng);
        return Ok(result);
    }

    [HttpGet("/api/v1/test/vietmap")]
    public async Task<IActionResult> GetAddressByLocation([FromQuery] double lat, [FromQuery] double lng)
    {
        var result = await _vietMapService.GetAddressByLocation(lat, lng);
        return Ok(result);
    }

    [HttpGet("/api/v1/test/pagination")]
    public async Task<IActionResult> GetPagination()
    {
        var response = new List<UserLocationDto>();
        var keys = await _redisService.GetKeysByPatternAsync("UserLocation:*");
        foreach (var key in keys)
        {
            var value = await _redisService.GetStringAsync(key);
            var userLocation = JsonSerializer.Deserialize<UserLocationDto>(value);
            response.Add(userLocation);
        }

        return Ok(response);
    }
}