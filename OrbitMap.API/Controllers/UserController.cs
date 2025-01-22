using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OrbitMap.API.Constants;
using OrbitMap.API.Helper;
using OrbitMap.API.Payload.Request.User;
using OrbitMap.API.Payload.Response.Result;
using OrbitMap.API.Payload.Response.User;
using OrbitMap.API.Services.Interface;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Controllers;

[ApiController]
[Route(ApiEndPointConstant.User.UserEndpoint)]
public class UserController : BaseController<UserController>
{
    private readonly IUserService _userService;

    public UserController(ILogger logger, IUserService userService) : base(logger)
    {
        _userService = userService;
    }

    [HttpPatch(ApiEndPointConstant.User.UserEndpoint)]
    [ProducesResponseType(typeof(ApiSuccessResult<UserDto>), StatusCodes.Status200OK)]
    public async Task<ApiResult<UserDto>> UpdateUser([FromForm] UpdateUserRequest request)
    {
        var result = await _userService.UpdateProfile(User.GetUsername(), request);
        return new ApiSuccessResult<UserDto>(result);
    }

    [HttpPatch(ApiEndPointConstant.User.Rank)]
    [ProducesResponseType(typeof(ApiSuccessResult<bool>), StatusCodes.Status200OK)]
    public async Task<ApiResult<bool>> UpdateRank([FromBody] UpdateRankRequest request)
    {
        var result = await _userService.UpdateRank(User.GetUsername(), request);
        return new ApiSuccessResult<bool>(result);
    }

    [HttpGet(ApiEndPointConstant.User.Profile)]
    [ProducesResponseType(typeof(ApiSuccessResult<MemberDto>), StatusCodes.Status200OK)]
    public async Task<ApiResult<MemberDto>> GetProfile()
    {
        var username = User.GetUsername();
        var result = await _userService.GetProfile(username);
        return new ApiSuccessResult<MemberDto>(result);
    }

    [HttpPatch(ApiEndPointConstant.User.UpdatePassword)]
    [ProducesResponseType(typeof(ApiSuccessResult<MemberDto>), StatusCodes.Status200OK)]
    public async Task<ApiResult<MemberDto>> UpdatePassword([Required] [FromBody] ChangePasswordRequest request)
    {
        var username = User.GetUsername();
        _logger.Information($"BEGIN: {nameof(UpdatePassword)} - {DateTime.UtcNow}");
        var result = await _userService.ChangePassword(username, request);
        _logger.Information($"END: {nameof(UpdatePassword)} - {DateTime.UtcNow}");
        return new ApiSuccessResult<MemberDto>(result);
    }
}