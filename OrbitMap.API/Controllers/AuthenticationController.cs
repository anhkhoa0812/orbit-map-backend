using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OrbitMap.API.Constants;
using OrbitMap.API.Payload.Request.Sms;
using OrbitMap.API.Payload.Request.User;
using OrbitMap.API.Payload.Response.Result;
using OrbitMap.API.Payload.Response.User;
using OrbitMap.API.Services.Interface;
using OrbitMap.API.Utils;
using OrbitMap.Domain.Utils;
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
    [ProducesResponseType(typeof(ApiSuccessResult<LoginResponse>), StatusCodes.Status200OK)]
    public async Task<ApiResult<LoginResponse>> Login([FromBody] [Required] LoginRequest loginRequest)
    {
        _logger.Information($"BEGIN: {nameof(Login)} - {TimeUtil.GetCurrentSEATime()}");
        var result = await _userService.Login(loginRequest);
        _logger.Information($"END: {nameof(Login)} - {TimeUtil.GetCurrentSEATime()}");
        return new ApiSuccessResult<LoginResponse>(result);
    }

    [HttpPost(ApiEndPointConstant.Authentication.SendOtp)]
    [ProducesResponseType(typeof(ApiSuccessResult<string>), StatusCodes.Status200OK)]
    public async Task<ApiResult<string>> SendOtp([FromBody] [Required] SendOtpRequest sendOtpRequest)
    {
        // _logger.Information($"BEGIN: {nameof(SendOtp)} - {TimeUtil.GetCurrentSEATime()}");
        // var result = await _smsService.SendOtpAsync(sendOtpRequest);
        // _logger.Information($"END: {nameof(SendOtp)} - {TimeUtil.GetCurrentSEATime()}");
        return new ApiSuccessResult<string>(sendOtpRequest.PhoneNumber);
    }

    [HttpPost(ApiEndPointConstant.Authentication.Register)]
    [ProducesResponseType(typeof(ApiSuccessResult<LoginResponse>), StatusCodes.Status200OK)]
    public async Task<ApiResult<LoginResponse>> Register([FromBody] [Required] RegisterRequest registerRequest)
    {
        _logger.Information($"BEGIN: {nameof(Register)} - {TimeUtil.GetCurrentSEATime()}");
        var result = await _userService.Register(registerRequest);
        _logger.Information($"END: {nameof(Register)} - {TimeUtil.GetCurrentSEATime()}");
        return new ApiSuccessResult<LoginResponse>(result);
    }

    [HttpPatch(ApiEndPointConstant.Authentication.ForgotPassword)]
    [ProducesResponseType(typeof(ApiSuccessResult<MemberDto>), StatusCodes.Status200OK)]
    public async Task<ApiResult<MemberDto>> ForgotPassword(
        [FromBody] [Required] ForgetPasswordRequest forgetPasswordRequest)
    {
        _logger.Information($"BEGIN: {nameof(ForgotPassword)} - {TimeUtil.GetCurrentSEATime()}");
        var result = await _userService.ForgetPassword(forgetPasswordRequest);
        _logger.Information($"END: {nameof(ForgotPassword)} - {TimeUtil.GetCurrentSEATime()}");
        return new ApiSuccessResult<MemberDto>(result);
    }
}