using AutoMapper;
using OrbitMap.API.Payload.Request.News;
using OrbitMap.API.Payload.Response.News;
using OrbitMap.API.Utils;
using OrbitMap.Domain.Entities;

namespace OrbitMap.API.Mapper;

public class NewsMapper : Profile
{
    public NewsMapper()
    {
        CreateMap<CreateNewsRequest, News>();
        CreateMap<News, NewsResponse>();
        CreateMap<News, NewsWithReactionResponse>();
        CreateMap<NewsReaction, NewsReactionResponse>();
        CreateMap<UpdateNewsRequest, News>().IgnoreAllNonExisting();
    }
}