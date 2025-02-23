using Microsoft.AspNetCore.Mvc;
using OrbitMap.API.Constants;
using OrbitMap.API.Payload.Request.User;
using OrbitMap.API.Payload.Response.Business;
using OrbitMap.API.Payload.Response.Hotel;
using OrbitMap.API.Payload.Response.Restaurant;
using OrbitMap.API.Payload.Response.Result;
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
    [ProducesResponseType(typeof(ApiSuccessResult<CreateBusinessResponse>), StatusCodes.Status200OK)]
    public async Task<ApiResult<CreateBusinessResponse>> CreateBusinessAsync(CreateBusinessRequest request)
    {
        _logger.Information($"BEGIN: {nameof(CreateBusinessAsync)} - {DateTime.UtcNow}");
        var result = await _businessService.CreateBusinessAsync(request);
        _logger.Information($"END: {nameof(CreateBusinessAsync)} - {DateTime.UtcNow}");
        return new ApiSuccessResult<CreateBusinessResponse>(result);
    }

    [HttpGet(ApiEndPointConstant.Business.Restaurant)]
    [ProducesResponseType(typeof(ApiSuccessResult<List<RestaurantItemDto>>), StatusCodes.Status200OK)]
    public async Task<ApiResult<List<RestaurantItemDto>>> GetNearestRestaurant([FromQuery] double latitude,
        [FromQuery] double longitude,
        [FromQuery] string location)
    {
        _logger.Information($"BEGIN: {nameof(GetNearestRestaurant)} - {DateTime.UtcNow}");
        var result = await _businessService.GetNearestRestaurant(latitude, longitude, location);
        _logger.Information($"END: {nameof(GetNearestRestaurant)} - {DateTime.UtcNow}");
        return new ApiSuccessResult<List<RestaurantItemDto>>(result);
    }

    [HttpGet(ApiEndPointConstant.Business.Hotel)]
    [ProducesResponseType(typeof(ApiSuccessResult<List<HotelResponse>>), StatusCodes.Status200OK)]
    public async Task<ApiResult<List<HotelResponse>>> GetNearestHotel([FromQuery] double latitude,
        [FromQuery] double longitude, [FromQuery] string location)
    {
        _logger.Information($"BEGIN: {nameof(GetNearestHotel)} - {DateTime.UtcNow}");
        var result = await _businessService.GetNearestHotel(latitude, longitude, location);
        _logger.Information($"END: {nameof(GetNearestHotel)} - {DateTime.UtcNow}");
        return new ApiSuccessResult<List<HotelResponse>>(result);
    }

    [HttpGet(ApiEndPointConstant.Business.BusinessEndpoint)]
    [ProducesResponseType(typeof(ApiSuccessResult<List<BusinessResponse>>), StatusCodes.Status200OK)]
    public async Task<ApiResult<List<BusinessResponse>>> GetBusinesses()
    {
        _logger.Information($"BEGIN: {nameof(GetBusinesses)} - {DateTime.UtcNow}");
        var result = await _businessService.GetBusinessesAsync();
        _logger.Information($"END: {nameof(GetBusinesses)} - {DateTime.UtcNow}");
        return new ApiSuccessResult<List<BusinessResponse>>(result);
    }
}