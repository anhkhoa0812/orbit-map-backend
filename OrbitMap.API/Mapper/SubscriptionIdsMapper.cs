using AutoMapper;
using OrbitMap.API.Payload.Response.OneSignal;
using OrbitMap.Domain.Entities;

namespace OrbitMap.API.Mapper;

public class SubscriptionIdsMapper : Profile
{
    public SubscriptionIdsMapper()
    {
        CreateMap<SubscriptionIds, SubscriptionIdsDto>();
    }
}