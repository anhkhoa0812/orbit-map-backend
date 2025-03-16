using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OrbitMap.API.Constants;
using OrbitMap.API.Helper;
using OrbitMap.API.Payload.Request.User;
using OrbitMap.API.Payload.Response.Location;
using OrbitMap.API.Payload.Response.Result;
using OrbitMap.API.Payload.Response.User;
using OrbitMap.API.Services.Interface;
using OrbitMap.API.Utils;
using OrbitMap.API.Validators;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Filter.FilterModel;
using OrbitMap.Domain.Paginate.Interfaces;
using OrbitMap.Domain.Utils;
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
        _logger.Information($"BEGIN: {nameof(UpdatePassword)} - {TimeUtil.GetCurrentSEATime()}");
        var result = await _userService.ChangePassword(username, request);
        _logger.Information($"END: {nameof(UpdatePassword)} - {TimeUtil.GetCurrentSEATime()}");
        return new ApiSuccessResult<MemberDto>(result);
    }

    [HttpGet(ApiEndPointConstant.User.Location)]
    [ProducesResponseType(typeof(ApiSuccessResult<List<LocationDto>>), StatusCodes.Status200OK)]
    public async Task<ApiResult<List<LocationDto>>> GetLocations()
    {
        var username = User.GetUsername();
        var result = await _userService.GetLocations(username);
        return new ApiSuccessResult<List<LocationDto>>(result);
    }

    [CustomAuthorize(ERoleEnum.Member)]
    [HttpDelete(ApiEndPointConstant.User.UserEndpoint)]
    [ProducesResponseType(typeof(ApiSuccessResult<bool>), StatusCodes.Status200OK)]
    public async Task<ApiResult<bool>> DeleteUser([FromBody] DeleteUserRequest request)
    {
        var username = User.GetUsername();
        var result = await _userService.DeleteUser(username, request);
        return new ApiSuccessResult<bool>(result);
    }

    [CustomAuthorize(ERoleEnum.Admin)]
    [HttpGet(ApiEndPointConstant.User.UserEndpoint)]
    [ProducesResponseType(typeof(ApiSuccessResult<IPaginate<MemberDto>>), StatusCodes.Status200OK)]
    public async Task<ApiResult<IPaginate<MemberDto>>> GetUsers([FromQuery] int page = 1, [FromQuery] int size = 30,
        [FromQuery] MemberFilter filter = null, [FromQuery] string sortBy = null, [FromQuery] bool isAsc = true)
    {
        var result = await _userService.GetUsers(page, size, filter, sortBy, isAsc);
        return new ApiSuccessResult<IPaginate<MemberDto>>(result);
    }
}