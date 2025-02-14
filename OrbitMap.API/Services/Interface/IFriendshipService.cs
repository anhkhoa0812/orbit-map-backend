using OrbitMap.API.Payload.Request.Friendship;
using OrbitMap.API.Payload.Response.Friendship;
using OrbitMap.API.Payload.Response.User;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Paginate.Interfaces;

namespace OrbitMap.API.Services.Interface;

public interface IFriendshipService
{
    Task<List<FriendResponse>> AddFriendAsync(AddFriendRequest request);

    Task<FriendResponse> UpdateFriendStatus(UpdateFriendStatusRequest request);

    Task<IPaginate<FriendWithUserResponse>> GetFriendsForUser(int page, int size, string? searchItem, string status);

    Task<IPaginate<FriendWithUserResponse>> GetRecommendFriendsForUser(string username, int page, int size,
        List<string> phoneNumbers);
}