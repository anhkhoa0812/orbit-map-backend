using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using OrbitMap.API.Services.Interface;
using OrbitMap.Domain.Configurations;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Services.Implement;

public class UploadService : BaseService<UploadService>, IUploadService
{
    private readonly Cloudinary _cloudinary;
    public UploadService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, IOptions<CloudinarySettings> options) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
        var settings = options.Value;
        _cloudinary = new Cloudinary(new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret));
    }

    public Task<ImageUploadResult> UploadImageAsync(string base64Image)
    {
        try
        {
            if (!string.IsNullOrEmpty(base64Image))
            {
                base64Image = base64Image.Trim();
                byte[] imageBytes = Convert.FromBase64String(base64Image);
                using (var stream = new MemoryStream(imageBytes))
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(Guid.NewGuid().ToString(), stream),
                        PublicId = Guid.NewGuid().ToString()
                    };
                    var uploadResult = _cloudinary.Upload(uploadParams);
                    return Task.FromResult(uploadResult);
                }
            }
            else
            {
                _logger.Error("Image is empty");
                throw new BadHttpRequestException("Image is empty");
            }
        }
        catch (Exception e)
        {
            _logger.Error($"Failed to upload image to Cloudinary: {e.Message}");
            throw new Exception("Failed to upload image to Cloudinary", e);
        }
    }
}