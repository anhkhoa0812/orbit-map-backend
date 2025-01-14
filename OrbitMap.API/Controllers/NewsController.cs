using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OrbitMap.API.Constants;
using OrbitMap.API.Helper;
using OrbitMap.API.Payload.Request.News;
using OrbitMap.API.Payload.Response.News;
using OrbitMap.API.Payload.Response.Result;
using OrbitMap.API.Services.Interface;
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

    // [CustomAuthorize(ERoleEnum.Admin)]
    [HttpPost(ApiEndPointConstant.News.NewsEndpoint)]
    [ProducesResponseType(typeof(ApiSuccessResult<NewsResponse>), StatusCodes.Status200OK)]
    public async Task<ApiResult<NewsResponse>> CreateNewsAsync([FromBody] CreateNewsRequest request)
    {
        var result = await _newsService.CreateNewsAsync(request);
        return new ApiSuccessResult<NewsResponse>(result);
    }

    [HttpGet(ApiEndPointConstant.News.NewsEndpoint)]
    [ProducesResponseType(typeof(ApiSuccessResult<List<NewsByTypeResponse>>), StatusCodes.Status200OK)]
    public async Task<ApiResult<List<NewsByTypeResponse>>> GetNewsByTypeAsync()
    {
        var username = User.GetUsername();
        var result = await _newsService.GetNewsAsync(username);
        return new ApiSuccessResult<List<NewsByTypeResponse>>(result);
    }

    [HttpPost(ApiEndPointConstant.News.NewsReaction)]
    [ProducesResponseType(typeof(ApiSuccessResult<NewsReactionResponse>), StatusCodes.Status200OK)]
    public async Task<ApiResult<NewsReactionResponse>> UpdateNewsAsync([Required] Guid id,
        [FromBody] ReactNewsRequest request)
    {
        var username = User.GetUsername();
        var result = await _newsService.ReactToNewsAsync(username, id, request);
        return new ApiSuccessResult<NewsReactionResponse>(result);
    }
}