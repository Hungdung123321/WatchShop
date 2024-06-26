using Microsoft.EntityFrameworkCore;
using WatchShop.Data;

namespace WatchShop.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new WatchShopContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<WatchShopContext>>()))
        {
            if (context == null || context.Watch == null)
            {
                throw new ArgumentNullException("Null WatchShopContext");
            }

            // Look for any movies.
            if (context.Watch.Any())
            {
                return;   // DB has been seeded
            }

            context.Watch.AddRange(
                new Watch
                {
                    Name = "Casio - Nam AE-1200WHD-1AVDF Size 45 X 42.1 Mm",
                    Price = 1114000,
                    sale = 26,
                    Brand = new Brand
                    {
                        Name = "Casio",
                        Description = "Casio là thương hiệu đồng hồ Nhật Bản nổi tiếng ra đời vào năm 1946.\r\nDẫn đầu phân khúc đồng hồ điện tử và pin/quartz giá rẻ.\r\nThiết kế hiện đại, thể thao, có cấu trúc chống sốc G-SHOCK bền bỉ độc quyền.\r\nTuổi thọ pin 2 - 10 năm.\r\nĐạt 6 giải thưởng thiết kế IF 2024",
                        LogoUrl = "https://www.watchstore.vn/images/products/manufactories/2024/small/cs-1362181789-1668935694-1712398047.webp",
                        Origin = "Japan"
                    },
                    GlassType = "Kính Nhựa",
                    MachineType = "Pin/Quartz",
                    WireMaterial = "Dây Thép Không Gỉ",
                    WaterResistant = "10atm",
                    Gender = "Nam",
                    FaceSize = "45 x 42.1 mm",
                    Pic1 = "https://www.watchstore.vn/images/products/others/2024/large/1-khung-sp-6676612-1785849039-1712554705.webp",
                    Pic2 = "https://www.watchstore.vn/images/products/others/2024/large/ae-1200whd-1avdf-7-1712554705.webp",
                    Pic3 = "https://www.watchstore.vn/images/products/others/2024/large/ae-1200whd-1avdf-6-1712554706.webp",
                    Pic4 = "https://www.watchstore.vn/images/products/others/2024/large/ae-1200whd-1avdf-1-1712554708.webp",
                },
                new Watch
                {
                    Name = "Orient - Nam RA-AA0B02R19B Size 42mm",
                    Price = 6400000,
                    sale = 20,
                    Brand = new Brand
                    {
                        Name = "Orient",
                        Description = "Orient là thương hiệu đồng hồ Nhật Bản nổi tiếng ra đời vào năm 1950.\r\nNổi bật với đồng hồ cơ Automatic giá tầm trung.\r\nĐặc trưng phong cách cổ điển và thiết kế lộ tim hở đáy.\r\nKhoảng trữ cót 40 - 60 tiếng.",
                        LogoUrl = "https://www.watchstore.vn/images/products/manufactories/2024/small/logo-orient-1712398047.webp",
                        Origin = "Japan"
                    },
                    GlassType = "Kính Khoáng",
                    MachineType = "Cơ/Automatic",
                    WireMaterial = "Dây Kim Loại",
                    WaterResistant = "5atm",
                    Gender = "Nam",
                    FaceSize = "42mm",
                    Pic1 = "https://www.watchstore.vn/images/products/others/2024/large/ra-aa0b02r19b-2081811590-287106387-1712554040.webp",
                    Pic2 = "https://www.watchstore.vn/images/products/others/2024/large/ra-aa0b02r19b-2-1712554038.webp",
                    Pic3 = "https://www.watchstore.vn/images/products/others/2024/large/ra-aa0b02r19b-3-1712554038.webp",
                    Pic4 = "https://www.watchstore.vn/images/products/others/2024/large/ra-aa0b02r19b-4-1712554039.webp",
                },
                new Watch
                {
                    Name = "Bentley - Nam BL1831-25MWNN Size 41mm",
                    Price = 7050000,
                    sale = 26,
                    Brand = new Brand
                    {
                        Name = "Bentley",
                        Description = "Bentley là thương hiệu đồng hồ Thụy Sĩ nổi tiếng, ra đời năm 1948.\r\nNổi bật với đồng hồ pin Quartz và cơ Automatic giá tầm trung.\r\nĐặc trưng thiết kế cổ điển, sang trọng.\r\nTuổi thọ Pin/Quart 2 - 3 năm, khoảng trữ cót 38 - 48 tiếng.",
                        LogoUrl = "https://www.watchstore.vn/images/products/manufactories/2024/small/logo-bentley-1712398047.webp",
                        Origin = "Germany"
                    },
                    GlassType = "Kính Sapphire",
                    MachineType = "Cơ/Automatic",
                    WireMaterial = "Dây Da",
                    WaterResistant = "5atm",
                    Gender = "Nam",
                    FaceSize = "41mm",
                    Pic1 = "https://www.watchstore.vn/images/products/2024/resized/bl1831-25mwnn-1-1654749789743-1712491813.webp",
                    Pic2 = "https://www.watchstore.vn/images/products/others/2024/large/bl1831-25mwnn-2-1654749814529-1712585306.webp",
                    Pic3 = "https://www.watchstore.vn/images/products/others/2024/large/bl1831-25mwnn-3-1654749821949-1712585307.webp",
                    Pic4 = "https://www.watchstore.vn/images/products/others/2024/large/bl1831-25mwnn-4-1654749825059-1712585307.webp",
                },
                new Watch
                {
                    Name = "Omega - Nam 310.63.42.50.10.001 Size 42mm",
                    Price = 585000000,
                    sale = 27,
                    Brand = new Brand
                    {
                        Name = "Omega",
                        Description = "Omega là thương hiệu đồng hồ xa xỉ tầm trung Thụy Sĩ, ra đời năm 1848.\r\nNổi bật với đồng hồ cơ Automatic, pin Quartz mức giá trung - cao cấp.\r\nThiết kế sang trọng, thanh lịch, bộ máy có độ chính xác cao đạt chứng nhận COSC.\r\nKhoảng trữ cót 40 - 80 tiếng, tuổi thọ pin 2 - 4 năm.",
                        LogoUrl = "https://www.watchstore.vn/images/products/manufactories/2024/small/logo-omega-1712398048.webp",
                        Origin = "Switzerland"
                    },
                    GlassType = "Kính Sapphire",
                    MachineType = "Cơ/Automatic",
                    WireMaterial = "Dây Da",
                    WaterResistant = "5atm",
                    Gender = "Nam",
                    FaceSize = "42mm",
                    Pic1 = "https://www.watchstore.vn/images/products/others/2024/large/52-1679281029743-1712661790.webp",
                    Pic2 = "https://bizweb.dktcdn.net/100/175/988/products/310-63-42-50-10-001-1.jpg?v=1660276021960",
                    Pic3 = "https://bizweb.dktcdn.net/100/175/988/products/310-63-42-50-10-001-1.jpg?v=1660276021960",
                    Pic4 = "https://cdn.luxshopping.vn/Thumnails/Uploads/News/omega-speedmaster-310-63-42-50-10-001-moonwatch-42.jpg.webp",
                },
                new Watch
                {
                    Name = "Citizen - Nam NJ0151-88M Size 40mm",
                    Price = 10340000,
                    sale = 25,
                    Brand = new Brand
                    {
                        Name = "Citizen",
                        Description = "Citizen là thương hiệu đồng hồ Nhật Bản nổi tiếng, ra đời năm 1918.\r\nNổi bật với đồng hồ năng lượng mặt trời Eco Drive độc quyền, giá tầm trung.\r\nThiết kế hiện đại, có hệ thống chống sốc độc quyền Parashock.\r\nTuổi thọ pin 8 - 10 năm.",
                        LogoUrl = "https://www.watchstore.vn/images/products/manufactories/2024/small/logo-citizen-1712398047.webp",
                        Origin = "Japan"
                    },
                    GlassType = "Kính Sapphire",
                    MachineType = "Cơ/Automatic",
                    WireMaterial = "Dây Thép Không Gỉ",
                    WaterResistant = "5atm",
                    Gender ="Nam",
                    FaceSize = "40mm",
                    Pic1 = "https://www.watchstore.vn/images/products/others/2024/large/nj0151-88m-2-1676685772596-1712591098.webp",
                    Pic2 = "https://www.watchstore.vn/images/products/others/2024/large/nj0151-88m-1-1676685767509-1712591098.webp",
                    Pic3 = "https://www.watchstore.vn/images/products/others/2024/large/nj0151-88m-3-1676685776403-1712591099.webp",
                    Pic4 = "https://www.watchstore.vn/images/products/others/2024/large/nj0151-88m-4-1676685779623-1712591099.webp",
                }
            );
            context.SaveChanges();
        }
    }
}