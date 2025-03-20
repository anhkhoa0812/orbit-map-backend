using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OrbitMap.API.Constants;
using OrbitMap.API.Helper;
using OrbitMap.API.Payload.Request.News;
using OrbitMap.API.Payload.Response.News;
using OrbitMap.API.Payload.Response.Result;
using OrbitMap.API.Services.Interface;
using OrbitMap.API.Validators;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Filter.FilterModel;
using OrbitMap.Domain.Paginate;
using OrbitMap.Domain.Paginate.Interfaces;
using OrbitMap.Domain.Utils;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Controllers;

[ApiController]
[Route(ApiEndPointConstant.News.NewsEndpoint)]
public class NewsController : BaseController<NewsController>
{
    private readonly INewsService _newsService;

    public NewsController(ILogger logger, INewsService newsService) : base(logger)
    {
        _newsService = newsService;
    }

    [CustomAuthorize(ERoleEnum.Admin)]
    [HttpPost(ApiEndPointConstant.News.NewsEndpoint)]
    [ProducesResponseType(typeof(ApiSuccessResult<NewsResponse>), StatusCodes.Status200OK)]
    public async Task<ApiResult<NewsResponse>> CreateNewsAsync([FromForm] CreateNewsRequest request)
    {
        var result = await _newsService.CreateNewsAsync(request);
        return new ApiSuccessResult<NewsResponse>(result);
    }

    [HttpGet(ApiEndPointConstant.News.NewsEndpoint)]
    [ProducesResponseType(typeof(ApiSuccessResult<List<NewsWithReactionResponse>>), StatusCodes.Status200OK)]
    public async Task<ApiResult<List<NewsWithReactionResponse>>> GetNewsAsync()
    {
        var username = User.GetUsername();
        var result = await _newsService.GetNewsAsync(username);
        return new ApiSuccessResult<List<NewsWithReactionResponse>>(result);
    }

    [HttpPatch(ApiEndPointConstant.News.NewsReaction)]
    [ProducesResponseType(typeof(ApiSuccessResult<NewsReactionResponse>), StatusCodes.Status200OK)]
    public async Task<ApiResult<NewsReactionResponse>> UpdateNewsAsync([Required] Guid id,
        [FromBody] ReactNewsRequest request)
    {
        var username = User.GetUsername();
        var result = await _newsService.ReactToNewsAsync(username, id, request);
        return new ApiSuccessResult<NewsReactionResponse>(result);
    }

    [HttpGet(ApiEndPointConstant.News.NewsPagination)]
    [ProducesResponseType(typeof(ApiSuccessResult<IPaginate<NewsResponse>>), StatusCodes.Status200OK)]
    public async Task<ApiResult<IPaginate<NewsResponse>>> GetNewsPaginationAsync([FromQuery] int page = 1,
        [FromQuery] int size = 30, [FromQuery] NewsFilter? filter = null, string? sortBy = null,
        [FromQuery] bool isAsc = true)
    {
        var result = await _newsService.GetAllNewsPaging(page, size, filter, sortBy, isAsc);
        return new ApiSuccessResult<IPaginate<NewsResponse>>(result);
    }

    [HttpDelete(ApiEndPointConstant.News.DeleteImage)]
    [ProducesResponseType(typeof(ApiSuccessResult<NewsResponse>), StatusCodes.Status200OK)]
    [CustomAuthorize(ERoleEnum.Admin)]
    public async Task<ApiResult<NewsResponse>> DeleteNewsAsync([Required] Guid id,
        [FromBody] DeleteImageNewsRequest request)
    {
        var result = await _newsService.DeleteNewsAsync(id, request);
        return new ApiSuccessResult<NewsResponse>(result);
    }

    [HttpGet(ApiEndPointConstant.News.NewsWithId)]
    [ProducesResponseType(typeof(ApiSuccessResult<NewsResponse>), StatusCodes.Status200OK)]
    public async Task<ApiResult<NewsResponse>> GetNewsByIdAsync([Required] Guid id)
    {
        _logger.Information($"BEGIN: {nameof(GetNewsByIdAsync)} - {TimeUtil.GetCurrentSEATime()}");
        var result = await _newsService.GetNewsByIdAsync(id);
        _logger.Information($"END: {nameof(GetNewsByIdAsync)} - {TimeUtil.GetCurrentSEATime()}");
        return new ApiSuccessResult<NewsResponse>(result);
    }


    [HttpPatch(ApiEndPointConstant.News.NewsWithId)]
    [ProducesResponseType(typeof(ApiSuccessResult<NewsResponse>), StatusCodes.Status200OK)]
    [CustomAuthorize(ERoleEnum.Admin)]
    public async Task<ApiResult<NewsResponse>> UpdateNewsAsync([Required] Guid id, [FromForm] UpdateNewsRequest request)
    {
        _logger.Information($"BEGIN: {nameof(UpdateNewsAsync)} - {TimeUtil.GetCurrentSEATime()}");
        var result = await _newsService.UpdateNewsAsync(id, request);
        _logger.Information($"END: {nameof(UpdateNewsAsync)} - {TimeUtil.GetCurrentSEATime()}");
        return new ApiSuccessResult<NewsResponse>(result);
    }
}