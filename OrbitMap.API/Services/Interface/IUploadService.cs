namespace OrbitMap.API.Services.Interface;

public interface IUploadService
{
    Task<string> UploadImageAsync(IFormFile file);
}