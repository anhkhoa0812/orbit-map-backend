namespace OrbitMap.API.Constants;

public static class ApiEndPointConstant
{
    public const string RootEndPoint = "/api";
    public const string ApiVersion = "/v1";
    public const string ApiEndpoint = RootEndPoint + ApiVersion;

    public static class Authentication
    {
        public const string AuthenticationEndpoint = ApiEndpoint + "/account";
        public const string Login = AuthenticationEndpoint + "/login";
        public const string Register = AuthenticationEndpoint + "/register";
        public const string SendOtp = AuthenticationEndpoint + "/otp";
        public const string ForgotPassword = AuthenticationEndpoint + "/password";
    }

    public static class User
    {
        public const string UserEndpoint = ApiEndpoint + "/users";
        public const string Rank = UserEndpoint + "/rank";
        public const string Profile = UserEndpoint + "/profile";
        public const string UpdatePassword = UserEndpoint + "/changepass";
    }

    public static class OneSignal
    {
        public const string OneSignalEndpoint = ApiEndpoint + "/onesignal";
        public const string SendNotification = OneSignalEndpoint + "/send";
        public const string SubscriptionId = OneSignalEndpoint + "/subscriptionId/{subscriptionId}";
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

    public static class Story
    {
        public const string StoryEndpoint = ApiEndpoint + "/stories";
        public const string StoryWithId = StoryEndpoint + "/{id}";
        public const string ReplyStory = StoryEndpoint + "/reply";
        public const string StoryByMonth = StoryEndpoint + "/month";
    }

    public static class Payment
    {
        public const string PaymentEndpoint = ApiEndpoint + "/payments";
    }

    public static class News
    {
        public const string NewsEndpoint = ApiEndpoint + "/news";
        public const string NewsWithId = NewsEndpoint + "/{id}";
        public const string NewsReaction = NewsEndpoint + "/reaction";
    }

    public static class Business
    {
        public const string BusinessEndpoint = ApiEndpoint + "/business";
    }
}