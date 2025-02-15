using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace OrbitMap.API.Utils;

public static class ImageUtil
{
    public static async Task<List<string>> ResizeImages(List<string> imageUrls)
    {
        if (!imageUrls.Any())
            throw new BadHttpRequestException("Không hình ảnh nào được tìm thấy");
        var result = new List<string>();
        foreach (var imageUrl in imageUrls)
        {
            byte[] imageBytes = await File.ReadAllBytesAsync(imageUrl);

            using (var image = Image.Load(imageBytes))
            {
                image.Mutate(
                    x => x.Resize(200, 200)
                );

                using (var ms = new MemoryStream())
                {
                    image.Save(ms, new JpegEncoder());

                    string base64Image = Convert.ToBase64String(ms.ToArray());
                    result.Add(base64Image);
                }
            }
        }

        return result;
    }

    public static async Task<string> ResizeImage(string imageUrl)
    {
        using (var httpClient = new HttpClient())
        {
            var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
            using (var image = Image.Load(imageBytes))
            {
                image.Mutate(
                    x => x.Resize(200, 200)
                );

                using (var ms = new MemoryStream())
                {
                    image.Save(ms, new JpegEncoder());

                    string base64Image = Convert.ToBase64String(ms.ToArray());
                    return $"data:image/jpeg;base64,{base64Image}";
                }
            }
        }
    }
}