using Microsoft.AspNetCore.Mvc;
using OrbitMap.API.Constants;
using OrbitMap.API.Payload.Response.OneSignal;
using OrbitMap.API.Payload.Response.Result;
using OrbitMap.API.Services.Implement;
using OrbitMap.API.Services.Interface;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Controllers;

[ApiController]
[Route(ApiEndPointConstant.OneSignal.OneSignalEndpoint)]
public class OneSignalController : BaseController<OneSignalController>
{
    private readonly IOneSignalService _oneSignalService;
    private readonly NotificationService _notificationService;

    public OneSignalController(ILogger logger, IOneSignalService oneSignalService,
        NotificationService notificationService) : base(logger)
    {
        _oneSignalService = oneSignalService;
        _notificationService = notificationService;
    }

    [HttpPost(ApiEndPointConstant.OneSignal.SendNotification)]
    [ProducesResponseType(typeof(ApiSuccessResult<ResultOneSignal>), StatusCodes.Status200OK)]
    public async Task<ApiResult<ResultOneSignal>> SendNotification(object param)
    {
        return new ApiSuccessResult<ResultOneSignal>(await _oneSignalService.SendNotification(param));
    }

    [HttpPost(ApiEndPointConstant.OneSignal.SubscriptionId)]
    [ProducesResponseType(typeof(ApiSuccessResult<SubscriptionIdsDto>), StatusCodes.Status200OK)]
    public async Task<ApiResult<SubscriptionIdsDto>> AddSubscriptionId(string subscriptionId)
    {
        var result = await _oneSignalService.AddSubscriptionIdAsync(subscriptionId, User.Identity.Name);
        return new ApiSuccessResult<SubscriptionIdsDto>(result);
    }


    [HttpPost(ApiEndPointConstant.OneSignal.OneSignalEndpoint + "/test")]
    [ProducesResponseType(typeof(SubscriptionIdsDto), StatusCodes.Status200OK)]
    public async Task<ApiResult<NoContentResult>> SendNotificationTest(string username, string message)
    {
        await _notificationService.SendNotificationToUser("", username, message);
        return new ApiSuccessResult<NoContentResult>(NoContent());
    }

    [HttpDelete(ApiEndPointConstant.OneSignal.SubscriptionId)]
    [ProducesResponseType(typeof(ApiSuccessResult<SubscriptionIdsDto>), StatusCodes.Status200OK)]
    public async Task<ApiResult<SubscriptionIdsDto>> RemoveSubscriptionId(string subscriptionId)
    {
        var username = User.Identity.Name;
        if (username == null)
        {
            return new ApiErrorResult<SubscriptionIdsDto>("Không tìm thấy user");
        }

        var result = await _oneSignalService.RemoveSubscriptionIdAsync(subscriptionId, User.Identity.Name);
        return new ApiSuccessResult<SubscriptionIdsDto>(result);
    }
}