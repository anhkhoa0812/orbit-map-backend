using AutoMapper;
using OrbitMap.API.Payload.Response.LastMessageChat;
using OrbitMap.Domain.Entities;

namespace OrbitMap.API.Mapper;

public class LastMessageChatMapper : Profile
{
    public LastMessageChatMapper()
    {
        CreateMap<LastMessageChat, LastMessageChatResponse>();
    }
}