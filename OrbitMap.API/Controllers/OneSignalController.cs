using Microsoft.AspNetCore.Mvc;
using OrbitMap.API.Constants;
using OrbitMap.API.Payload.Response.OneSignal;
using OrbitMap.API.Payload.Response.Result;
using OrbitMap.API.Services.Interface;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Controllers;

[ApiController]
[Route(ApiEndPointConstant.OneSignal.OneSignalEndpoint)]
public class OneSignalController : BaseController<OneSignalController>
{
    private readonly IOneSignalService _oneSignalService;
    public OneSignalController(ILogger logger, IOneSignalService oneSignalService) : base(logger)
    {
        _oneSignalService = oneSignalService;
    }
    
    [HttpPost(ApiEndPointConstant.OneSignal.SendNotification)]
    [ProducesResponseType(typeof(ResultOneSignal), StatusCodes.Status200OK)]
    public async Task<IActionResult> SendNotification(object param)
    {
        return Ok(await _oneSignalService.SendNotification(param));
    }
    
    [HttpPost(ApiEndPointConstant.OneSignal.AddPlayerId)]
    [ProducesResponseType(typeof(PlayerIdsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ApiResult<PlayerIdsDto>> AddPlayerId(string playerId)
    {
        var result = await _oneSignalService.AddPlayerId(playerId, User.Identity.Name);
        return new ApiSuccessResult<PlayerIdsDto>(result);
    }
}