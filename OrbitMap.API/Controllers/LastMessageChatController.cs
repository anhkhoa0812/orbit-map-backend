using Microsoft.AspNetCore.Mvc;
using OrbitMap.API.Constants;
using OrbitMap.API.Helper;
using OrbitMap.API.Payload.Response.LastMessageChat;
using OrbitMap.API.Payload.Response.Result;
using OrbitMap.API.Services.Interface;
using OrbitMap.API.Utils;
using OrbitMap.Domain.Paginate.Interfaces;
using OrbitMap.Domain.Utils;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Controllers;

[ApiController]
[Route(ApiEndPointConstant.LastMessageChat.LastMessageChatEndpoint)]
public class LastMessageChatController : BaseController<LastMessageChatController>
{
    private readonly ILastMessageChatService _lastMessageChatService;

    public LastMessageChatController(ILogger logger, ILastMessageChatService lastMessageChatService) : base(logger)
    {
        _lastMessageChatService = lastMessageChatService;
    }

    [HttpGet(ApiEndPointConstant.LastMessageChat.LastMessageChatEndpoint)]
    [ProducesResponseType(typeof(ApiResult<IPaginate<LastMessageChatResponse>>), StatusCodes.Status200OK)]
    public async Task<ApiResult<IPaginate<LastMessageChatResponse>>> GetLastMessageChat([FromQuery] int page,
        [FromQuery] int size)
    {
        var currentUsername = User.GetUsername();
        _logger.Information($"BEGIN: {nameof(GetLastMessageChat)} - {TimeUtil.GetCurrentSEATime()}");
        var result = await _lastMessageChatService.GetLastMessageChat(page, size, currentUsername);
        _logger.Information($"END: {nameof(GetLastMessageChat)} - {TimeUtil.GetCurrentSEATime()}");
        return new ApiSuccessResult<IPaginate<LastMessageChatResponse>>(result);
    }
}