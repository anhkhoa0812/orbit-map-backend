using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OrbitMap.API.Constants;
using OrbitMap.API.Helper;
using OrbitMap.API.Payload.Request.Friendship;
using OrbitMap.API.Payload.Response.Friendship;
using OrbitMap.API.Payload.Response.Result;
using OrbitMap.API.Payload.Response.User;
using OrbitMap.API.Services.Interface;
using OrbitMap.Domain.Paginate.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Controllers;

[ApiController]
[Route(ApiEndPointConstant.Friendship.FriendshipEndpoint)]
public class FriendshipController : BaseController<FriendshipController>
{
    private readonly IFriendshipService _friendshipService;

    public FriendshipController(ILogger logger, IFriendshipService friendshipService) : base(logger)
    {
        _friendshipService = friendshipService;
    }

    [HttpPost(ApiEndPointConstant.Friendship.AddFriend)]
    [ProducesResponseType(typeof(ApiSuccessResult<List<FriendResponse>>), StatusCodes.Status200OK)]
    public async Task<ApiResult<List<FriendResponse>>> AddFriend([Required] AddFriendRequest request)
    {
        var result = await _friendshipService.AddFriendAsync(request);
        return new ApiSuccessResult<List<FriendResponse>>(result);
    }

    [HttpPatch(ApiEndPointConstant.Friendship.UpdateFriendStatus)]
    [ProducesResponseType(typeof(ApiSuccessResult<FriendResponse>), StatusCodes.Status200OK)]
    public async Task<ApiResult<FriendResponse>> UpdateFriendStatus([Required] UpdateFriendStatusRequest request)
    {
        var result = await _friendshipService.UpdateFriendStatus(request);
        return new ApiSuccessResult<FriendResponse>(result);
    }

    [HttpGet(ApiEndPointConstant.Friendship.GetFriendsForUser)]
    [ProducesResponseType(typeof(ApiSuccessResult<IPaginate<FriendWithUserResponse>>), StatusCodes.Status200OK)]
    public async Task<ApiResult<IPaginate<FriendWithUserResponse>>> GetFriendsForUser([Required] int page = 1,
        [Required] int size = 30, [Required] string status = null, string? searchItem = null)
    {
        var result = await _friendshipService.GetFriendsForUser(page, size, searchItem, status);
        return new ApiSuccessResult<IPaginate<FriendWithUserResponse>>(result);
    }

    [HttpGet(ApiEndPointConstant.Friendship.GetRecommendFriendsForUser)]
    [ProducesResponseType(typeof(ApiSuccessResult<IPaginate<FriendWithUserResponse>>), StatusCodes.Status200OK)]
    public async Task<ApiResult<IPaginate<FriendWithUserResponse>>> GetRecommendFriendsForUser([Required] int page = 1,
        [Required] int size = 30, [Required] [FromQuery] List<string> phoneNumbers = null)
    {
        var username = User.GetUsername();
        var result = await _friendshipService.GetRecommendFriendsForUser(username, page, size, phoneNumbers);
        return new ApiSuccessResult<IPaginate<FriendWithUserResponse>>(result);
    }
}