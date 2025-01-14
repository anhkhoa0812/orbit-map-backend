using OrbitMap.API.Payload.Request.News;
using OrbitMap.API.Payload.Response.News;

namespace OrbitMap.API.Services.Interface;

public interface INewsService
{
    Task<NewsResponse> CreateNewsAsync(CreateNewsRequest request);
    Task<List<NewsByTypeResponse>> GetNewsAsync(string username);

    Task<NewsReactionResponse> ReactToNewsAsync(string username, Guid newsId, ReactNewsRequest request);
}