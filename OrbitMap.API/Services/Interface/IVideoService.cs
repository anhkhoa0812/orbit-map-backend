using OrbitMap.API.Payload.Request.Story;

namespace OrbitMap.API.Services.Interface;

public interface IVideoService
{
    Task<string> CreateVideoTimeLapse(CreateStoryTimeLapseRequest request);
}