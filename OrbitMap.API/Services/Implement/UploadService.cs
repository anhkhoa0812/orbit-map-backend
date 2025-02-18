using AutoMapper;
using FFMpegCore;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
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

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new BadHttpRequestException("Không tìm thấy file");
        }

        var allowedExtensions = new[] { ".jpeg", ".png", ".jpg", ".gif", ".bmp", ".webp" };
        var extension = Path.GetExtension(file.FileName).ToLower();

        if (!allowedExtensions.Contains(extension))
            throw new InvalidOperationException(
                "Chỉ các định dạng tệp txt, .pdf, .doc, .docx, .xls, .xlsx, .ppt, và .pptx được phép tải lên.");
        try
        {
            var minio = new MinioClient()
                .WithEndpoint(_awsSettings.EndPoint)
                .WithCredentials(_awsSettings.AccessKey, _awsSettings.SecretKey)
                .Build();

            var headers = new Dictionary<string, string>
            {
                { "x-amz-acl", "public-read" }
            };
            var objectName = $"{Guid.NewGuid().ToString()}{extension}";
            var result = await minio.PutObjectAsync(new PutObjectArgs()
                .WithBucket(_awsSettings.BucketName)
                .WithObject(objectName)
                .WithStreamData(file.OpenReadStream())
                .WithObjectSize(file.Length)
                .WithContentType("image/jpeg")
                .WithHeaders(headers)
            );
            if (result == null)
                throw new MinioException("Failed to upload image");
            // var reqParams = new Dictionary<string, string>(StringComparer.Ordinal)
            //     { { "response-content-type", "image/jpeg" } };
            // var presignedUrlArgs = new PresignedGetObjectArgs()
            //     .WithBucket(_awsSettings.BucketName) // Your bucket name
            //     .WithObject(result.ObjectName)
            //     .WithExpiry(604800)
            //     .WithHeaders(reqParams);
            // var url = await minio.PresignedGetObjectAsync(presignedUrlArgs);
            // return url;
            return $"https://{_awsSettings.EndPoint}/{_awsSettings.BucketName}/{objectName}";
        }
        catch (Exception e)
        {
            _logger.Error($"Failed to upload image: {e.Message}");
            throw new Exception("Failed to upload image", e);
        }
    }

    public async Task<string> UploadVideoAsync(IFormFile file, bool isStory)
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

        if (isStory)
        {
            var duration = GetVideoDurationAsync(file);
            if (duration.Result.TotalSeconds > 5)
                throw new BadHttpRequestException("Video dài quá 5 giây");
        }

        try
        {
            var minio = new MinioClient()
                .WithEndpoint(_awsSettings.EndPoint)
                .WithCredentials(_awsSettings.AccessKey, _awsSettings.SecretKey)
                .Build();
            var objectName = $"{Guid.NewGuid().ToString()}{extension}";
            var headers = new Dictionary<string, string>
            {
                { "x-amz-acl", "public-read" }
            };
            var result = await minio.PutObjectAsync(new PutObjectArgs()
                .WithBucket(_awsSettings.BucketName)
                .WithObject(objectName)
                .WithStreamData(file.OpenReadStream())
                .WithObjectSize(file.Length)
                .WithContentType("video/mp4")
                .WithHeaders(headers)
            );
            if (result == null)
                throw new MinioException("Failed to upload video");
            var url = $"https://{_awsSettings.EndPoint}/{_awsSettings.BucketName}/{objectName}";
            return url;
        }
        catch (Exception e)
        {
            _logger.Error($"Failed to upload video: {e.Message}");
            throw new Exception("Failed to upload video", e);
        }
    }


    private async Task<TimeSpan> GetVideoDurationAsync(IFormFile file)
    {
        GlobalFFOptions.Configure(options => options.BinaryFolder = @"C:\ffmpeg\bin");
        var tempPath = Path.GetTempFileName();
        try
        {
            using (var stream = new FileStream(tempPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var mediaInfo = await FFProbe.AnalyseAsync(tempPath);
            return mediaInfo.Duration;
        }
        finally
        {
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
        }
    }
}