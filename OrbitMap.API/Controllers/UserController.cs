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
    public async Task<ApiResult<UserDto>> UpdateUser([FromBody] UpdateUserRequest request)
    {
        var result = await _userService.UpdateProfile(User.GetUsername(), request);
        return new ApiSuccessResult<UserDto>(result);
    }
}