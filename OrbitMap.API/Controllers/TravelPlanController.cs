using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OrbitMap.API.Constants;
using OrbitMap.API.Payload.Request.TravelPlan;
using OrbitMap.API.Payload.Response.Result;
using OrbitMap.API.Payload.Response.TravelPlan;
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
[Route(ApiEndPointConstant.TravelPlan.TravelPlanEndpoint)]
public class TravelPlanController : BaseController<TravelPlanController>
{
    private readonly ITravelPlanService _travelPlanService;

    public TravelPlanController(ILogger logger, ITravelPlanService travelPlanService) : base(logger)
    {
        _travelPlanService = travelPlanService;
    }

    [HttpPost(ApiEndPointConstant.TravelPlan.TravelPlanEndpoint)]
    [ProducesResponseType(typeof(ApiSuccessResult<TravelPlanResponse>), StatusCodes.Status200OK)]
    public async Task<ApiResult<TravelPlanResponse>> CreateTravelPlan(
        [FromBody] [Required] CreateTravelPlanRequest request)
    {
        _logger.Information($"BEGIN: {nameof(CreateTravelPlan)} - {TimeUtil.GetCurrentSEATime()}");
        var result = await _travelPlanService.CreateTravelPlanAsync(request);
        _logger.Information($"END: {nameof(CreateTravelPlan)} - {TimeUtil.GetCurrentSEATime()}");
        return new ApiSuccessResult<TravelPlanResponse>(result);
    }

    [HttpGet(ApiEndPointConstant.TravelPlan.TravelPlanEndpoint)]
    [ProducesResponseType(typeof(ApiSuccessResult<List<TravelPlanResponse>?>), StatusCodes.Status200OK)]
    public async Task<ApiResult<List<TravelPlanResponse>?>> GetTravelPlan([FromQuery] [Required] string locationName)
    {
        _logger.Information($"BEGIN: {nameof(GetTravelPlan)} - {TimeUtil.GetCurrentSEATime()}");
        var result = await _travelPlanService.GetTravelPlanAsync(locationName);
        _logger.Information($"END: {nameof(GetTravelPlan)} - {TimeUtil.GetCurrentSEATime()}");
        return new ApiSuccessResult<List<TravelPlanResponse>?>(result);
    }

    [HttpGet(ApiEndPointConstant.TravelPlan.TravelPlanPaging)]
    [ProducesResponseType(typeof(ApiSuccessResult<IPaginate<TravelPlanResponse>?>), StatusCodes.Status200OK)]
    [CustomAuthorize(ERoleEnum.Admin, ERoleEnum.Member)]
    public async Task<ApiResult<IPaginate<TravelPlanResponse>?>> GetAllTravelPlanPaging(
        [FromQuery] int page = 1,
        [FromQuery] int size = 30,
        [FromQuery] TravelPlanFilter? filter = null,
        [FromQuery] string? sortBy = null,
        [FromQuery] bool isAsc = true
    )
    {
        _logger.Information($"BEGIN: {nameof(GetAllTravelPlanPaging)} - {TimeUtil.GetCurrentSEATime()}");
        var result = await _travelPlanService.GetAllTravelPlanPaging(page, size, filter, sortBy, isAsc);
        _logger.Information($"END: {nameof(GetAllTravelPlanPaging)} - {TimeUtil.GetCurrentSEATime()}");
        return new ApiSuccessResult<IPaginate<TravelPlanResponse>?>(result);
    }

    [HttpGet(ApiEndPointConstant.TravelPlan.TravelPlanById)]
    [ProducesResponseType(typeof(ApiSuccessResult<TravelPlanResponse>), StatusCodes.Status200OK)]
    [CustomAuthorize(ERoleEnum.Admin, ERoleEnum.Member)]
    public async Task<ApiResult<TravelPlanResponse>> GetTravelPlanById(Guid id)
    {
        _logger.Information($"BEGIN: {nameof(GetTravelPlanById)} - {TimeUtil.GetCurrentSEATime()}");
        var result = await _travelPlanService.GetTravelPlanByIdAsync(id);
        _logger.Information($"END: {nameof(GetTravelPlanById)} - {TimeUtil.GetCurrentSEATime()}");
        return new ApiSuccessResult<TravelPlanResponse>(result);
    }
}