using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Utils;

namespace OrbitMap.Domain.Persistent.Configurations;

public class NewsConfiguration : IEntityTypeConfiguration<News>
{
    public void Configure(EntityTypeBuilder<News> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(n => n.Type)
            .HasConversion(
                v => v.ToString(),
                v => (ENewsType)Enum.Parse(typeof(ENewsType), v)
            );
        builder.HasData(new List<News>(
            new[]
            {
                new News()
                {
                    Id = Guid.Parse("8254d0a9-6b2b-41e4-ac19-d56be30a5727"),
                    Title = "TỪ 1/11 BẢO TÀNG LỊCH SỬ QUÂN SỰ MIỄN PHÍ VÉ",
                    Content =
                        "Bảo tàng tại Nam Từ Liêm, Hà Nội, mở cửa 1/11 và miễn phí vé trong tháng đầu. Dự án 2.500 tỷ đồng trải rộng trên 74ha, với điểm nhấn là Tháp Chiến thắng cao 45m - tượng trưng cho năm 1945. Ngoài trưng bày lịch sử chiến tranh, bảo tàng còn mang đến trải nghiệm về cuộc đấu tranh của Quân đội Nhân dân Việt Nam.",
                    CreatedDate = TimeUtil.GetCurrentSEATime(),
                    Type = ENewsType.HeaderBanner,
                    BannerImage =
                        "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783633/3b07c90d-3c5c-4600-a569-274d93804790.png",
                    UselessReactionCount = 0,
                    UsefulReactionCount = 0,
                    BusinessName = "Báo Thái Bình",
                    BusinessAddress = "Thái Bình",
                    BusinessImage =
                        "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783632/2828588e-984c-47fe-acdd-77eca703c9d3.png",
                    ImageUrls = new List<string>()
                    {
                        "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png"
                    },
                    ExpirationDate = TimeUtil.GetCurrentSEATime().AddMonths(1)
                },
                new News()
                {
                    Id = Guid.Parse("fc456c95-6ffe-4408-a64e-1750c96e38a0"),
                    Title = "Tour leo núi Fansipan 2N1Đ (Xuất phát từ Sa Pa)",
                    Content =
                        "Fansipan – ngọn núi cao nhất Việt Nam, không chỉ được mệnh danh là Nóc nhà Đông Dương mà còn là biểu tượng chinh phục của sức trẻ cùng lòng quyết tâm cháy bỏng. Với độ cao 3143m, Fansipan là ngọn núi cao nhất Việt Nam và là mơ ước của những người đam mê chinh phục.",
                    CreatedDate = TimeUtil.GetCurrentSEATime(),
                    Type = ENewsType.BannersOnPage,
                    UselessReactionCount = 0,
                    UsefulReactionCount = 0,
                    BusinessName = "Viettrekking",
                    BusinessAddress = "Hà Nội",
                    BusinessImage =
                        "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783632/2828588e-984c-47fe-acdd-77eca703c9d3.png",
                    ImageUrls = new List<string>()
                    {
                        "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png"
                    },
                    ExpirationDate = TimeUtil.GetCurrentSEATime().AddMonths(1)
                },
            }
        ));
    }
}