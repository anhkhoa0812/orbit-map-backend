using OrbitMap.API.Payload.Request.News;
using OrbitMap.API.Payload.Response.News;
using OrbitMap.Domain.Filter.FilterModel;
using OrbitMap.Domain.Paginate.Interfaces;

namespace OrbitMap.API.Services.Interface;

public interface INewsService
{
    Task<NewsResponse> CreateNewsAsync(CreateNewsRequest request);
    Task<List<NewsWithReactionResponse>> GetNewsAsync(string username);

    Task<NewsReactionResponse> ReactToNewsAsync(string username, Guid newsId, ReactNewsRequest request);

    Task<IPaginate<NewsResponse>> GetAllNewsPaging(int page, int size, NewsFilter? filter, string? sortBy, bool isAsc);

    Task<NewsResponse> DeleteNewsAsync(Guid newsId, DeleteImageNewsRequest request);
}