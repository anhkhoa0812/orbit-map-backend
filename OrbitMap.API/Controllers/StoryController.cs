using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OrbitMap.API.Constants;
using OrbitMap.API.Helper;
using OrbitMap.API.Payload.Request.Message;
using OrbitMap.API.Payload.Request.Story;
using OrbitMap.API.Payload.Response.Message;
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
    private readonly IVideoService _videoService;

    public StoryController(ILogger logger, IStoryService storyService, IVideoService videoService) : base(logger)
    {
        _storyService = storyService;
        _videoService = videoService;
    }

    [HttpPost(ApiEndPointConstant.Story.StoryEndpoint)]
    [ProducesResponseType(typeof(ApiSuccessResult<CreateStoryResponse>), StatusCodes.Status200OK)]
    public async Task<ApiResult<CreateStoryResponse>> AddStory([FromForm] CreateStoryRequest request)
    {
        _logger.Information($"BEGIN: {nameof(AddStory)} - {DateTime.UtcNow}");
        var result = await _storyService.CreateStoryAsync(User.GetUsername(), request);
        _logger.Information($"END: {nameof(AddStory)} - {DateTime.UtcNow}");
        return new ApiSuccessResult<CreateStoryResponse>(result);
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

    [HttpPost(ApiEndPointConstant.Story.ReplyStory)]
    [ProducesResponseType(typeof(ApiSuccessResult<MessageDto>), StatusCodes.Status200OK)]
    public async Task<ApiResult<MessageDto>> ReplyStory([FromBody] CreateMessageDto request)
    {
        _logger.Information($"BEGIN: {nameof(ReplyStory)} - {DateTime.UtcNow}");
        var result = await _storyService.ReplyStoryAsync(User.GetUsername(), request);
        _logger.Information($"END: {nameof(ReplyStory)} - {DateTime.UtcNow}");
        return new ApiSuccessResult<MessageDto>(result);
    }

    [HttpGet(ApiEndPointConstant.Story.StoryByMonth)]
    [ProducesResponseType(typeof(ApiSuccessResult<List<StoryByMonthResponse>>), StatusCodes.Status200OK)]
    public async Task<ApiResult<List<StoryByMonthResponse>>> GetStoriesByMonth()
    {
        _logger.Information($"BEGIN: {nameof(GetStoriesByMonth)} - {DateTime.UtcNow}");
        var result = await _storyService.GetStoriesByMonthAsync(User.GetUsername());
        _logger.Information($"END: {nameof(GetStoriesByMonth)} - {DateTime.UtcNow}");
        return new ApiSuccessResult<List<StoryByMonthResponse>>(result);
    }

    [HttpPost(ApiEndPointConstant.Story.StoryTimeLapse)]
    [ProducesResponseType(typeof(ApiSuccessResult<string>), StatusCodes.Status200OK)]
    public async Task<ApiResult<string>> GetStoryTimeLapse([Required] CreateStoryTimeLapseRequest request)
    {
        _logger.Information($"BEGIN: {nameof(GetStoryTimeLapse)} - {DateTime.UtcNow}");
        var result = await _videoService.CreateVideoTimeLapse(request);
        _logger.Information($"END: {nameof(GetStoryTimeLapse)} - {DateTime.UtcNow}");
        return new ApiSuccessResult<string>(result);
    }

    [HttpGet(ApiEndPointConstant.Story.StoryImage)]
    [ProducesResponseType(typeof(ApiSuccessResult<List<string>>), StatusCodes.Status200OK)]
    public async Task<ApiResult<List<string>>> GetAllImageUrlStory()
    {
        var username = User.GetUsername();
        _logger.Information($"BEGIN: {nameof(GetAllImageUrlStory)} - {DateTime.UtcNow}");
        var result = await _storyService.GetAllImageUrlStoryAsync(username);
        _logger.Information($"END: {nameof(GetAllImageUrlStory)} - {DateTime.UtcNow}");
        return new ApiSuccessResult<List<string>>(result);
    }
}