using OrbitMap.API.Payload.Response.LastMessageChat;
using OrbitMap.Domain.Paginate.Interfaces;

namespace OrbitMap.API.Services.Interface;

public interface ILastMessageChatService
{
    Task<IPaginate<LastMessageChatResponse>> GetLastMessageChat(int page, int size, string currentUsername);
}