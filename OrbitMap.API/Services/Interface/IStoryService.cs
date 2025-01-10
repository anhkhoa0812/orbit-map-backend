using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OrbitMap.API.Payload.Request.Story;
using OrbitMap.API.Payload.Response.Story;

namespace OrbitMap.API.Services.Interface;

public interface IStoryService
{
    Task<StoryResponse> CreateStoryAsync(string username, CreateStoryRequest createStoryRequest);
    
    Task<List<StoryResponse>> GetStoriesByUserAsync(string username, string? searchTerm);
    
    Task<NoContentResult> DeleteStoryAsync(string username, Guid id); 
}