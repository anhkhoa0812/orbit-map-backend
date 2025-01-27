using Microsoft.AspNetCore.Mvc;
using OrbitMap.API.Constants;
using OrbitMap.API.Payload.Request.User;
using OrbitMap.API.Payload.Response.Result;
using OrbitMap.API.Payload.Response.User;
using OrbitMap.API.Services.Interface;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Controllers;

[ApiController]
[Route(ApiEndPointConstant.Business.BusinessEndpoint)]
public class BusinessController : BaseController<BusinessController>
{
    private readonly IBusinessService _businessService;

    public BusinessController(ILogger logger, IBusinessService businessService) : base(logger)
    {
        _businessService = businessService;
    }

    [HttpPost(ApiEndPointConstant.Business.BusinessEndpoint)]
    [ProducesResponseType(typeof(ApiSuccessResult<BusinessResponse>), StatusCodes.Status200OK)]
    public async Task<ApiResult<BusinessResponse>> CreateBusinessAsync(CreateBusinessRequest request)
    {
        _logger.Information($"BEGIN: {nameof(CreateBusinessAsync)} - {DateTime.UtcNow}");
        var result = await _businessService.CreateBusinessAsync(request);
        _logger.Information($"END: {nameof(CreateBusinessAsync)} - {DateTime.UtcNow}");
        return new ApiSuccessResult<BusinessResponse>(result);
    }
}