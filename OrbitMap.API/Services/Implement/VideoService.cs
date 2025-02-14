using AutoMapper;
using OrbitMap.API.Services.Interface;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using Xabe.FFmpeg;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Services.Implement
{
    // public class VideoService : BaseService<VideoService>, IVideoService
    // {
    //     public VideoService(IUnitOfWork<OrbitMapContext> unitOfWork,
    //         ILogger logger,
    //         IMapper mapper,
    //         IHttpContextAccessor httpContextAccessor)
    //         : base(unitOfWork, logger, mapper, httpContextAccessor)
    //     {
    //     }
    //
    //     /// <summary>
    //     /// Creates a timelapse video from an array of image URLs.
    //     /// Each image is scaled to even dimensions and then concatenated.
    //     /// </summary>
    //     /// <param name="images">An array of image URLs (or local paths) to include.</param>
    //     /// <returns>A string representing the FFmpeg arguments used (for logging/debugging).</returns>
    //     public async Task<string> CreateVideoTimeLapse(string[] images)
    //     {
    //         if (images == null || !images.Any())
    //             throw new BadHttpRequestException("Không có ảnh nào được chọn");
    //
    //         // Set the path to your FFmpeg executables.
    //         FFmpeg.SetExecutablesPath("C:\\ffmeg\\bin");
    //
    //         // Build the input parameters: each image is added as an input.
    //         // Example: -y -i "image1" -i "image2" -i "image3" ...
    //         StringBuilder ffmpegArgsBuilder = new StringBuilder("-y ");
    //         foreach (var image in images)
    //         {
    //             ffmpegArgsBuilder.Append($"-i \"{image}\" ");
    //         }
    //
    //         // Define the output file path.
    //         var outputFile = Path.Combine(@"C:\Videos", $"timelapse_{DateTime.Now:yyyyMMddHHmmss}.mp4");
    //
    //         // Dynamically build the filter_complex string.
    //         // For each input, apply a scaling filter that forces even dimensions and sets the SAR to 1.
    //         // Then, concatenate all the filtered streams.
    //         // For example, for 3 images:
    //         //   [0:v]scale=trunc(iw/2)*2:trunc(ih/2)*2,setsar=1[v0];
    //         //   [1:v]scale=trunc(iw/2)*2:trunc(ih/2)*2,setsar=1[v1];
    //         //   [2:v]scale=trunc(iw/2)*2:trunc(ih/2)*2,setsar=1[v2];
    //         //   [v0][v1][v2]concat=n=3:v=1:a=0[out]
    //         StringBuilder filterComplexBuilder = new StringBuilder();
    //         for (int i = 0; i < images.Length; i++)
    //         {
    //             filterComplexBuilder.Append($"[{i}:v]scale=trunc(iw/2)*2:trunc(ih/2)*2,setsar=1[v{i}];");
    //         }
    //
    //         // Append all the filtered video streams in order for concatenation.
    //         for (int i = 0; i < images.Length; i++)
    //         {
    //             filterComplexBuilder.Append($"[v{i}]");
    //         }
    //
    //         filterComplexBuilder.Append($"concat=n={images.Length}:v=1:a=0[out]");
    //
    //         string filterComplex = filterComplexBuilder.ToString();
    //
    //         // Append the filter_complex, mapping, and output options to the FFmpeg arguments.
    //         // This tells FFmpeg to use the filtered (and concatenated) output,
    //         // encode with libx264, and set the pixel format to yuv420p.
    //         ffmpegArgsBuilder.Append(
    //             $"-filter_complex \"{filterComplex}\" -map \"[out]\" -c:v libx264 -pix_fmt yuv420p \"{outputFile}\"");
    //
    //         string ffmpegArgs = ffmpegArgsBuilder.ToString();
    //
    //         // Start the conversion process.
    //         var conversion = FFmpeg.Conversions.New();
    //         IConversionResult result = await conversion.Start(ffmpegArgs);
    //
    //         // Return the arguments used (or consider returning the output file path).
    //         return result.Arguments.ToString();
    //     }
    // }
    public class VideoService : BaseService<VideoService>, IVideoService
    {
        public VideoService(IUnitOfWork<OrbitMapContext> unitOfWork,
            ILogger logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        /// <summary>
        /// Creates a timelapse video from an array of remote image URLs.
        /// Each image is displayed for a specified duration, with a crossfade transition effect between images.
        /// </summary>
        /// <param name="images">An array of image URLs.</param>
        /// <returns>The output video file path.</returns>
        public async Task<string> CreateVideoTimeLapse(string[] images)
        {
            if (images == null || !images.Any())
                throw new BadHttpRequestException("Không có ảnh nào được chọn");

            return await Task.Run(async () =>
            {
                // Set durations (in seconds)
                double displayDuration = 2.0; // Time each image is fully displayed
                double transitionDuration = 1.0; // Duration of the crossfade transition effect
                double fps = 60; // Frames per second

                int displayFrames = (int)(displayDuration * fps);
                int transitionFrames = (int)(transitionDuration * fps);

                // Define output video file path.
                var outputFile = Path.Combine(@"C:\Videos", $"timelapse_{DateTime.Now:yyyyMMddHHmmss}.mp4");

                // Create an HttpClient (consider reusing a singleton HttpClient in production)
                using HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

                // Download and decode the first image to determine the frame size.
                Mat previousImage = await DownloadAndProcessImageAsync(httpClient, images[0]);
                if (previousImage.Empty())
                    throw new Exception($"Unable to load image: {images[0]}");

                // Ensure even dimensions (many codecs require even widths/heights)
                int width = previousImage.Width;
                int height = previousImage.Height;
                if (width % 2 != 0) width--;
                if (height % 2 != 0) height--;
                Size frameSize = new Size(width, height);

                // Resize the first image if necessary.
                if (previousImage.Width != frameSize.Width || previousImage.Height != frameSize.Height)
                {
                    Cv2.Resize(previousImage, previousImage, frameSize);
                }

                // Initialize VideoWriter (using H.264 codec).
                int fourcc = VideoWriter.FourCC('H', '2', '6', '4');
                using VideoWriter writer = new VideoWriter(outputFile, fourcc, fps, frameSize, true);
                if (!writer.IsOpened())
                    throw new Exception("Unable to open video writer.");

                // Write display frames for the first image.
                for (int i = 0; i < displayFrames; i++)
                {
                    writer.Write(previousImage);
                }

                // Process remaining images.
                for (int i = 1; i < images.Length; i++)
                {
                    // Download and decode the current image.
                    using Mat currentImage = await DownloadAndProcessImageAsync(httpClient, images[i]);
                    if (currentImage.Empty())
                    {
                        Console.WriteLine($"Warning: Unable to load image: {images[i]}");
                        continue;
                    }

                    // Resize current image if needed.
                    if (currentImage.Width != frameSize.Width || currentImage.Height != frameSize.Height)
                    {
                        Cv2.Resize(currentImage, currentImage, frameSize);
                    }

                    // Create transition frames using a crossfade effect.
                    for (int t = 0; t < transitionFrames; t++)
                    {
                        double alpha = (double)t / transitionFrames;
                        using Mat blended = new Mat();
                        Cv2.AddWeighted(previousImage, 1.0 - alpha, currentImage, alpha, 0, blended);
                        writer.Write(blended);
                    }

                    // Write display frames for the current image.
                    for (int d = 0; d < displayFrames; d++)
                    {
                        writer.Write(currentImage);
                    }

                    // Dispose of the previous image and update it for the next transition.
                    previousImage.Dispose();
                    previousImage = currentImage.Clone();
                }

                previousImage.Dispose();
                return outputFile;
            });
        }

        /// <summary>
        /// Downloads an image from the specified URL and decodes it into a Mat.
        /// </summary>
        /// <param name="httpClient">The HttpClient used for the request.</param>
        /// <param name="url">The image URL.</param>
        /// <returns>A Mat containing the decoded image.</returns>
        private async Task<Mat> DownloadAndProcessImageAsync(HttpClient httpClient, string url)
        {
            try
            {
                using HttpResponseMessage response =
                    await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                using Stream stream = await response.Content.ReadAsStreamAsync();
                using MemoryStream ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                byte[] imageBytes = ms.ToArray();
                Mat image = Cv2.ImDecode(imageBytes, ImreadModes.Color);
                return image;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading image from URL: {url}. Exception: {ex.Message}");
                return new Mat();
            }
        }
    }
}