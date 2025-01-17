using AutoMapper;
using OrbitMap.API.Payload.Response.Message;
using OrbitMap.Domain.Entities;

namespace OrbitMap.API.Mapper;

public class MessageMapper : Profile
{
    public MessageMapper()
    {
        CreateMap<Message, MessageDto>();
    }
}