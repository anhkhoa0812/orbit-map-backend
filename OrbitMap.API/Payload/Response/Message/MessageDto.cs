using OrbitMap.API.Payload.Response.Story;

namespace OrbitMap.API.Payload.Response.Message;

public class MessageDto
{
    public Guid Id { get; set; }
    public string SenderUsername { get; set; }
    public string RecipientUsername { get; set; }
    public string Content { get; set; }
    public Guid StoryId { get; set; }
    public StoryResponse Story { get; set; }
    public DateTime? DateRead { get; set; }
    public DateTimeOffset MessageSent { get; set; }
}