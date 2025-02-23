using AutoMapper;
using OrbitMap.API.Payload.Request.User;
using OrbitMap.API.Payload.Response.Friendship;
using OrbitMap.API.Payload.Response.Location;
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
        CreateMap<LoginRequest, Member>();
        CreateMap<User, LoginResponse>()
            .ForMember(opt => opt.Role, src => src.MapFrom(src => src.Role.Name));
        CreateMap<RegisterRequest, Member>();
        CreateMap<UpdateUserRequest, User>().IgnoreAllNonExisting();
        CreateMap<UpdateUserRequest, Member>().IgnoreAllNonExisting();
        CreateMap<Member, MemberDto>();
        CreateMap<CreateBusinessRequest, Business>();
        CreateMap<Member, UserLocationDto>();
        CreateMap<Member, FriendWithUserResponse>();
    }
}