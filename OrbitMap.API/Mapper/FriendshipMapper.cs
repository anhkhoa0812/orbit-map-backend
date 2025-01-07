using AutoMapper;
using OrbitMap.API.Payload.Response.Friendship;
using OrbitMap.Domain.Entities;

namespace OrbitMap.API.Mapper;

public class FriendshipMapper : Profile
{
    public FriendshipMapper()
    {
        CreateMap<Friendship, FriendResponse>();
    }
}