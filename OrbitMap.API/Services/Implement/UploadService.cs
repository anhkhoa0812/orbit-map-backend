using AutoMapper;
using Microsoft.Extensions.Options;
using OrbitMap.API.Services.Interface;
using OrbitMap.Domain.Configurations;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Services.Implement;

public class UploadService : BaseService<UploadService>, IUploadService
{
    public UploadService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger,
        mapper, httpContextAccessor)
    {
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new BadHttpRequestException("Không tìm thấy file");
        }

        var allowedExtensions = new[] { ".jgeg", ".png", ".jpg", ".gif", ".bmp", ".webp" };
        var extension = Path.GetExtension(file.FileName).ToLower();

        if (!allowedExtensions.Contains(extension))
            throw new InvalidOperationException(
                "Chỉ các định dạng tệp txt, .pdf, .doc, .docx, .xls, .xlsx, .ppt, và .pptx được phép tải lên.");

        try
        {
            using var fileStream = file.OpenReadStream();
            byte[] fileBytes = new byte[file.Length];
            await fileStream.ReadAsync(fileBytes, 0, (int)file.Length);
            string directoryPath = @"C:\Pictures";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string fileName = $"{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(@"C:\Pictures", fileName);

            await using (var outputFileStream = new FileStream(filePath, FileMode.Create))
            {
                await outputFileStream.WriteAsync(fileBytes, 0, fileBytes.Length);
            }

            return $"https://api.stemlabs.store/pictures/{fileName}";
        }
        catch (Exception e)
        {
            _logger.Error($"Failed to upload image: {e.Message}");
            throw new Exception("Failed to upload image", e);
        }
    }
}