namespace OrbitMap.API.Payload.Response.LastMessageChat;

public class LastMessageChatResponse
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public string SenderUsername { get; set; }
    public Guid RecipientId { get; set; }
    public string RecipientUsername { get; set; }
    public string Content { get; set; }
    public DateTime MessageLastDate { get; set; }
    public string GroupName { get; set; }
    public bool IsRead { get; set; } = false;
}