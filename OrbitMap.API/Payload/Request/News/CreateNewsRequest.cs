using System.ComponentModel.DataAnnotations;
using OrbitMap.API.Helper;
using OrbitMap.Domain.Enums;

namespace OrbitMap.API.Payload.Request.News;

public class CreateNewsRequest
{
    [Required]
    [MaxLength(500, ErrorMessage = "Title không được dài quá 500 ký tự")]
    public string Title { get; set; }

    [Required] public string Content { get; set; }

    [Required]
    [MaxLength(255, ErrorMessage = "BusinessName không được dài quá 255 ký tự")]
    public string BusinessName { get; set; }

    [Required]
    [MaxLength(255, ErrorMessage = "BusinessAddress không được dài quá 255 ký tự")]
    public string BusinessAddress { get; set; }

    public string? BannerBase64Image { get; set; }
    [Required] public string BusinessBase64Image { get; set; }
    [Required] public ENewsType Type { get; set; }
    public List<string>? Base64Images { get; set; }

    [Required] [DateTimeChecking] public DateTime ExpirationDate { get; set; }
}