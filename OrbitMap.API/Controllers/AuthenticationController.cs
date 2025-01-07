using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OrbitMap.API.Constants;
using OrbitMap.API.Payload.Request.Sms;
using OrbitMap.API.Payload.Request.User;
using OrbitMap.API.Payload.Response.Result;
using OrbitMap.API.Payload.Response.User;
using OrbitMap.API.Services.Interface;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Controllers;

[ApiController]
[Route(ApiEndPointConstant.Authentication.AuthenticationEndpoint)]
public class AuthenticationController : BaseController<AuthenticationController>
{
    private readonly IUserService _userService;
    private readonly ISmsService _smsService;
    public AuthenticationController(ILogger logger, IUserService userService, ISmsService smsService) : base(logger)
    {
        _userService = userService;
        _smsService = smsService;
    }

    [HttpPost(ApiEndPointConstant.Authentication.Login)]
    [ProducesResponseType(typeof(ApiResult<LoginResponse>), StatusCodes.Status200OK)]
    public async Task<ApiResult<LoginResponse>> Login([FromBody] [Required] LoginRequest loginRequest)
    {
        _logger.Information($"BEGIN: {nameof(Login)} - {DateTime.UtcNow}");
        var result = await _userService.Login(loginRequest);
        _logger.Information($"END: {nameof(Login)} - {DateTime.UtcNow}");
        return new ApiSuccessResult<LoginResponse>(result);
    }
    [HttpPost(ApiEndPointConstant.Authentication.SendOtp)]
    [ProducesResponseType(typeof(ApiResult<string>), StatusCodes.Status200OK)]
    public async Task<ApiResult<string>> SendOtp([FromBody] [Required] SendOtpRequest sendOtpRequest)
    {
        _logger.Information($"BEGIN: {nameof(SendOtp)} - {DateTime.UtcNow}");
        var result = await _smsService.SendOtpAsync(sendOtpRequest);
        _logger.Information($"END: {nameof(SendOtp)} - {DateTime.UtcNow}");
        return new ApiSuccessResult<string>(result);
    }
    [HttpPost(ApiEndPointConstant.Authentication.Register)]
    [ProducesResponseType(typeof(ApiResult<LoginResponse>), StatusCodes.Status200OK)]
    public async Task<ApiResult<LoginResponse>> Register([FromBody] [Required] RegisterRequest registerRequest)
    {
        _logger.Information($"BEGIN: {nameof(Register)} - {DateTime.UtcNow}");
        var result = await _userService.Register(registerRequest);
        _logger.Information($"END: {nameof(Register)} - {DateTime.UtcNow}");
        return new ApiSuccessResult<LoginResponse>(result);
    }
}