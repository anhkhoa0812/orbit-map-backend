using AutoMapper;
using OrbitMap.API.Payload.Response.Location;
using OrbitMap.Domain.Entities;

namespace OrbitMap.API.Mapper;

public class LocationMapper : Profile
{
    public LocationMapper()
    {
        CreateMap<Location, LocationDto>();
    }
}