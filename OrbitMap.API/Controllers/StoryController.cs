using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OrbitMap.API.Constants;
using OrbitMap.API.Helper;
using OrbitMap.API.Payload.Request.Story;
using OrbitMap.API.Payload.Response.Result;
using OrbitMap.API.Payload.Response.Story;
using OrbitMap.API.Services.Interface;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Controllers;

[ApiController]
[Route(ApiEndPointConstant.Story.StoryEndpoint)]
public class StoryController : BaseController<StoryController>
{
    private readonly IStoryService _storyService;

    public StoryController(ILogger logger, IStoryService storyService) : base(logger)
    {
        _storyService = storyService;
    }

    [HttpPost(ApiEndPointConstant.Story.StoryEndpoint)]
    [ProducesResponseType(typeof(ApiSuccessResult<StoryResponse>), StatusCodes.Status200OK)]
    public async Task<ApiResult<StoryResponse>> AddStory([FromForm] [Required] CreateStoryRequest request)
    {
        _logger.Information($"BEGIN: {nameof(AddStory)} - {DateTime.UtcNow}");
        var result = await _storyService.CreateStoryAsync(User.GetUsername(), request);
        _logger.Information($"END: {nameof(AddStory)} - {DateTime.UtcNow}");
        return new ApiSuccessResult<StoryResponse>(result);
    }

    [HttpGet(ApiEndPointConstant.Story.StoryEndpoint)]
    [ProducesResponseType(typeof(ApiSuccessResult<List<StoryResponse>>), StatusCodes.Status200OK)]
    public async Task<ApiResult<List<StoryResponse>>> GetStories(string? searchTerm)
    {
        _logger.Information($"BEGIN: {nameof(GetStories)} - {DateTime.UtcNow}");
        var result = await _storyService.GetStoriesByUserAsync(User.GetUsername(), searchTerm);
        _logger.Information($"END: {nameof(GetStories)} - {DateTime.UtcNow}");
        return new ApiSuccessResult<List<StoryResponse>>(result);
    }

    [HttpDelete(ApiEndPointConstant.Story.StoryWithId)]
    [ProducesResponseType(typeof(ApiSuccessResult<NoContentResult>), StatusCodes.Status200OK)]
    public async Task<ApiResult<NoContentResult>> DeleteStory([Required] Guid id)
    {
        _logger.Information($"BEGIN: {nameof(DeleteStory)} - {DateTime.UtcNow}");
        await _storyService.DeleteStoryAsync(User.GetUsername(), id);
        _logger.Information($"END: {nameof(DeleteStory)} - {DateTime.UtcNow}");
        return new ApiSuccessResult<NoContentResult>(NoContent());
    }
}