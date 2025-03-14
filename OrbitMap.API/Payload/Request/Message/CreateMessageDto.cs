namespace OrbitMap.API.Payload.Request.Message;

public class CreateMessageDto
{
    public string RecipientUsername { get; set; }

    public string Content { get; set; }

    public bool IsSticker { get; set; }
    public Guid? StoryId { get; set; }
}