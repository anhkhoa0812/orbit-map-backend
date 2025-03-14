using AutoMapper;
using OrbitMap.API.Payload.Response.TravelPlan;
using OrbitMap.Domain.Entities;

namespace OrbitMap.API.Mapper;

public class TravelPlanMapper : Profile
{
    public TravelPlanMapper()
    {
        CreateMap<TravelPlan, TravelPlanResponse>()
            .ForMember(dest => dest.TravelPlanDays, opt => opt.MapFrom(src => src.TravelPlanDays));
        CreateMap<TravelPlanDay, TravelPlanDayResponse>()
            .ForMember(dest => dest.TravelPlanItems, opt => opt.MapFrom(src => src.TravelPlanItems));
        CreateMap<TravelPlanItem, TravelPlanItemResponse>();
    }
}