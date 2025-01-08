namespace OrbitMap.API.Constants;

public static class ApiEndPointConstant
{
    static ApiEndPointConstant()
    {
    }

    public const string RootEndPoint = "/api";
    public const string ApiVersion = "/v1";
    public const string ApiEndpoint = RootEndPoint + ApiVersion;
    
    public static class Authentication
    {
        public const string AuthenticationEndpoint = ApiEndpoint + "/account";
        public const string Login = AuthenticationEndpoint + "/login";
        public const string UpdatePassword = AuthenticationEndpoint + "/changepass";
        public const string Register = AuthenticationEndpoint + "/register";
        public const string SendOtp = AuthenticationEndpoint + "/otp";
    }

    public static class OneSignal
    {
        public const string OneSignalEndpoint = ApiEndpoint + "/onesignal";
        public const string SendNotification = OneSignalEndpoint + "/send";
        public const string AddPlayerId = OneSignalEndpoint + "/addplayerId";
    }

    public static class Friendship
    {
        public const string FriendshipEndpoint = ApiEndpoint + "/friendships";
        public const string AddFriend = FriendshipEndpoint + "/add";
        public const string UpdateFriendStatus = FriendshipEndpoint + "/update";
        public const string GetFriendsForUser = FriendshipEndpoint;
    }

    public static class LastMessageChat
    {
        public const string LastMessageChatEndpoint = ApiEndpoint + "/last-message-chat";
        
        
    }
}