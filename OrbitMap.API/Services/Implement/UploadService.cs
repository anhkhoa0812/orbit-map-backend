using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using AutoMapper;
using FFMpegCore;
using Microsoft.Extensions.Options;
using OrbitMap.API.Services.Interface;
using OrbitMap.Domain.Configurations;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Services.Implement;

public class UploadService : BaseService<UploadService>, IUploadService
{
    private readonly FileStorageSettings _settings;
    private readonly AwsSettings _awsSettings;

    public UploadService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, IOptions<AwsSettings> awsOptions,
        IOptions<FileStorageSettings> options) : base(unitOfWork, logger,
        mapper, httpContextAccessor)
    {
        _settings = options.Value;
        _awsSettings = awsOptions.Value;
    }

    // public async Task<string> UploadImageAsync(IFormFile file)
    // {
    //     if (file == null || file.Length == 0)
    //     {
    //         throw new BadHttpRequestException("Không tìm thấy file");
    //     }
    //
    //     var allowedExtensions = new[] { ".jgeg", ".png", ".jpg", ".gif", ".bmp", ".webp" };
    //     var extension = Path.GetExtension(file.FileName).ToLower();
    //
    //     if (!allowedExtensions.Contains(extension))
    //         throw new InvalidOperationException(
    //             "Chỉ các định dạng tệp txt, .pdf, .doc, .docx, .xls, .xlsx, .ppt, và .pptx được phép tải lên.");
    //     var credential = new BasicAWSCredentials(_awsSettings.AccessKey, _awsSettings.SecretKey);
    //     var config = new AmazonS3Config()
    //     {
    //         ServiceURL = "https://s3-hcm5-r1.longvan.net",
    //         RegionEndpoint = RegionEndpoint.USEast1,
    //         ForcePathStyle = true,
    //     };
    //     try
    //     {
    //         using (var client = new AmazonS3Client(credential, config))
    //         {
    //             using (var newMemoryStream = new MemoryStream())
    //             {
    //                 await file.CopyToAsync(newMemoryStream);
    //                 TransferUtilityUploadRequest uploadRequest = new TransferUtilityUploadRequest();
    //                 uploadRequest.AutoCloseStream = false;
    //                 uploadRequest.BucketName = "new-bucket-e12c7fa5";
    //                 uploadRequest.InputStream = newMemoryStream;
    //                 uploadRequest.Key = file.FileName;
    //                 uploadRequest.PartSize = 50 * 1024 * 1024;
    //                 TransferUtility ut = new TransferUtility(client);
    //                 await ut.UploadAsync(uploadRequest);
    //                 // PutObjectRequest putObjectRequest = new PutObjectRequest
    //                 // {
    //                 //     BucketName = "new-bucket-e12c7fa5",
    //                 //     Key = file.FileName,
    //                 //     InputStream = newMemoryStream,
    //                 //     CannedACL = S3CannedACL.PublicRead,
    //                 // };
    //                 // await client.PutObjectAsync(putObjectRequest);
    //                 return "kkk";
    //             }
    //         }
    //     }
    //     catch (Exception e)
    //     {
    //         _logger.Error($"Failed to upload image: {e.Message}");
    //         throw new Exception("Failed to upload image", e);
    //     }
    // }
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
            if (!Directory.Exists(_settings.ImagePath))
            {
                Directory.CreateDirectory(_settings.ImagePath);
            }

            string fileName = $"{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(_settings.ImagePath, fileName);

            await using (var outputFileStream = new FileStream(filePath, FileMode.Create))
            {
                await outputFileStream.WriteAsync(fileBytes, 0, fileBytes.Length);
            }

            return $"{_settings.ImagePathUrl}{fileName}";
        }
        catch (Exception e)
        {
            _logger.Error($"Failed to upload image: {e.Message}");
            throw new Exception("Failed to upload image", e);
        }
    }

    public async Task<string> UploadVideoAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new BadHttpRequestException("Không tìm thấy file");
        }

        var extension = Path.GetExtension(file.FileName).ToLower();
        if (!extension.Equals(".mp4"))
        {
            throw new InvalidOperationException("Chỉ các định dạng tệp mp4 được phép tải lên.");
        }

        var duration = GetVideoDurationAsync(file);
        if (duration.Result.TotalSeconds > 5)
            throw new BadHttpRequestException("Video dài quá 5 giây");
        try
        {
            using var fileStream = file.OpenReadStream();
            byte[] fileBytes = new byte[file.Length];
            await fileStream.ReadAsync(fileBytes, 0, (int)file.Length);

            if (!Directory.Exists(_settings.VideoPath))
            {
                Directory.CreateDirectory(_settings.VideoPath);
            }

            string fileName = $"{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(_settings.VideoPath, fileName);

            await using (var outputFileStream = new FileStream(filePath, FileMode.Create))
            {
                await outputFileStream.WriteAsync(fileBytes, 0, fileBytes.Length);
            }

            return $"{_settings.VideoPathUrl}{fileName}";
        }
        catch (Exception e)
        {
            _logger.Error($"Failed to upload video: {e.Message}");
            throw new Exception("Failed to upload video", e);
        }
    }

    private async Task<TimeSpan> GetVideoDurationAsync(IFormFile file)
    {
        var temPath = Path.GetTempFileName();

        using (var stream = new FileStream(temPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var mediaInfo = await FFProbe.AnalyseAsync(temPath);
        File.Delete(temPath);

        return mediaInfo.Duration;
    }
}