using AutoMapper;
using OrbitMap.API.Payload.Response.Business;
using OrbitMap.API.Payload.Response.User;
using OrbitMap.Domain.Entities;

namespace OrbitMap.API.Mapper;

public class BusinessMapper : Profile
{
    public BusinessMapper()
    {
        CreateMap<Business, BusinessResponse>();
        CreateMap<Business, CreateBusinessResponse>();
    }
}