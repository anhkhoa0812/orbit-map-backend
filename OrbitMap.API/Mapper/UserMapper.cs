using AutoMapper;
using OrbitMap.API.Payload.Request.User;
using OrbitMap.API.Payload.Response.User;
using OrbitMap.Domain.Entities;

namespace OrbitMap.API.Mapper;

public class UserMapper: Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDto>();
        CreateMap<LoginRequest, User>();
        CreateMap<User, LoginResponse>()
            .ForMember(opt => opt.Role, src => src.MapFrom(src => src.Role.Name));
    }
}