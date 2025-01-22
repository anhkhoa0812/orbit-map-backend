namespace OrbitMap.API.Payload.Response.Story;

public class StoryByMonthResponse
{
    public int Month { get; set; }
    public int Year { get; set; }
    public List<StoryResponse> Stories { get; set; }
}