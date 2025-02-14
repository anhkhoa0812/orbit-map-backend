namespace OrbitMap.API.Services.Interface;

public interface IVideoService
{
    Task<string> CreateVideoTimeLapse(string[] images);
}