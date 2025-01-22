namespace OrbitMap.API.Payload.Response.LastMessageChat;

public class LastMessageChatResponse
{
    public string SenderUsername { get; set; }
    public string RecipientUsername { get; set; }
    public string? SenderAvatarUrl { get; set; }
    public string? RecipientAvatarUrl { get; set; }
    public string Content { get; set; }
    public DateTime MessageLastDate { get; set; }
    public string GroupName { get; set; }
    public bool IsRead { get; set; } = false;
    public string HumanizedTime { get; set; }
}