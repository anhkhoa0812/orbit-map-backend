using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrbitMap.API.Helper;
using OrbitMap.API.Payload.Response.User;
using OrbitMap.API.Services.Interface;
using OrbitMap.API.SignalR;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;
using Member = AutoMapper.Execution.Member;

namespace OrbitMap.API.Controllers;

[ApiController]
[Route("/api/v1/passport")]
public class TestController : BaseController<TestController>
{
    private readonly ICraftMyPdfService _craftMyPdfService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork<OrbitMapContext> _unitOfWork;
    private readonly PresenceTracker _tracker;

    public TestController(ILogger logger, ICraftMyPdfService craftMyPdfService, IMapper mapper,
        IUnitOfWork<OrbitMapContext> unitOfWork, PresenceTracker tracker) :
        base(logger)
    {
        _craftMyPdfService = craftMyPdfService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _tracker = tracker;
    }

    // [HttpGet]
    // public async Task<IActionResult> GeneratePassport()
    // {
    //     var result = await _craftMyPdfService.GeneratePassport(User.GetUsername());
    //     return Ok(result);
    // }

    [HttpGet("/connections")]
    public async Task<IActionResult> GetConnections()
    {
        var username = User.GetUsername();
        var friends = await GetUsersOnlineAsync(username);
        var allConnections = new List<string>();

        // Lặp qua từng bạn bè và lấy danh sách connections, đồng thời kiểm tra null
        foreach (var friend in friends)
        {
            var connections = await _tracker.GetConnectionsForUser(friend.Username);
            if (connections != null && connections.Any())
            {
                allConnections.AddRange(connections);
            }
        }

        return Ok(allConnections);
    }

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

    [HttpGet("/onlineUser")]
    public async Task<IActionResult> GetOnlineUsers()
    {
        var onlineUsers = await _tracker.GetOnlineUsers();
        return Ok(onlineUsers);
    }
}