using AutoMapper;
using OrbitMap.Domain.Paginate;
using OrbitMap.Domain.Paginate.Interfaces;

namespace OrbitMap.API.Mapper;

public class PaginateMapper : Profile
{
    public PaginateMapper()
    {
        CreateMap(typeof(IPaginate<>), typeof(IPaginate<>)).ConvertUsing(typeof(PaginateConverter<,>));
    }
}