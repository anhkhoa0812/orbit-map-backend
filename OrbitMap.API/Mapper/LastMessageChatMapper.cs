using AutoMapper;
using OrbitMap.API.Payload.Response.LastMessageChat;
using OrbitMap.Domain.Entities;

namespace OrbitMap.API.Mapper;

public class LastMessageChatMapper : Profile
{
    public LastMessageChatMapper()
    {
        CreateMap<LastMessageChat, LastMessageChatResponse>()
            .ForMember(x => x.SenderUsername, opt => opt.MapFrom(x => x.LastMessageChatDocument.SenderUsername))
            .ForMember(x => x.RecipientUsername, opt => opt.MapFrom(x => x.LastMessageChatDocument.RecipientUsername))
            .ForMember(x => x.Content, opt => opt.MapFrom(x => x.LastMessageChatDocument.Content))
            .ForMember(x => x.MessageLastDate, opt => opt.MapFrom(x => x.LastMessageChatDocument.MessageLastDate))
            .ForMember(x => x.GroupName, opt => opt.MapFrom(x => x.LastMessageChatDocument.GroupName))
            .ForMember(x => x.IsRead, opt => opt.MapFrom(x => x.LastMessageChatDocument.IsRead))
            .ForMember(x => x.RecipientUsername, opt => opt.MapFrom(x => x.LastMessageChatDocument.RecipientUsername))
            .ForMember(x => x.SenderAvatarUrl, opt => opt.MapFrom(x => x.LastMessageChatDocument.SenderAvatarUrl));
    }
}