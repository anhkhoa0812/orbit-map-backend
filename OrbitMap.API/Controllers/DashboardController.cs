using Microsoft.AspNetCore.Mvc;
using OrbitMap.API.Constants;
using OrbitMap.API.Payload.Response.Dashboard;
using OrbitMap.API.Payload.Response.Result;
using OrbitMap.API.Services.Interface;
using OrbitMap.API.Validators;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Utils;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Controllers;

[ApiController]
[Route(ApiEndPointConstant.Dashboard.DashboardEndpoint)]
public class DashboardController : BaseController<DashboardController>
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(ILogger logger, IDashboardService dashboardService) : base(logger)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet(ApiEndPointConstant.Dashboard.DashboardByDay)]
    [ProducesResponseType(typeof(ApiSuccessResult<DashboardResponseByDay>), StatusCodes.Status200OK)]
    [CustomAuthorize(ERoleEnum.Admin)]
    public async Task<ApiResult<DashboardResponseByDay>> GetDashboardByDay([FromQuery] DateTime date)
    {
        _logger.Information($"BEGIN: {nameof(GetDashboardByDay)} - {TimeUtil.GetCurrentSEATime()}");
        var result = await _dashboardService.GetDashboardByDayAsync(date);
        _logger.Information($"END: {nameof(GetDashboardByDay)} - {TimeUtil.GetCurrentSEATime()}");
        return new ApiSuccessResult<DashboardResponseByDay>(result);
    }
}