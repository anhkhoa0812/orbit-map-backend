using AutoMapper;
using OrbitMap.API.Payload.Request.Story;
using OrbitMap.API.Payload.Response.Story;
using OrbitMap.Domain.Entities;

namespace OrbitMap.API.Mapper;

public class StoryMapper : Profile
{
    public StoryMapper()
    {
        CreateMap<CreateStoryRequest, Story>();
        CreateMap<Story, StoryResponse>()
            .ForMember(x => x.Username, opt => opt.MapFrom(x => x.Member.Username))
            .ForMember(x => x.AvatarUrl, opt => opt.MapFrom(x => x.Member.AvatarUrl));
    }
}