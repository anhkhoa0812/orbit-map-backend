using AutoMapper;
using OrbitMap.API.Payload.Request.User;
using OrbitMap.API.Payload.Response.User;
using OrbitMap.API.Utils;
using OrbitMap.Domain.Entities;

namespace OrbitMap.API.Mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDto>();
        CreateMap<Member, UserDto>();
        CreateMap<LoginRequest, User>();
        CreateMap<User, LoginResponse>()
            .ForMember(opt => opt.Role, src => src.MapFrom(src => src.Role.Name));
        CreateMap<RegisterRequest, User>();
        CreateMap<UpdateUserRequest, User>().IgnoreAllNonExisting();
        CreateMap<Member, MemberDto>();
        CreateMap<CreateBusinessRequest, Business>();
        CreateMap<Business, BusinessResponse>();
    }
}