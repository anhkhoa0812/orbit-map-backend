using CloudinaryDotNet.Actions;

namespace OrbitMap.API.Services.Interface;

public interface IUploadService
{
    Task<ImageUploadResult> UploadImageAsync(string base64Image);
}