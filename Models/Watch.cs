using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using WatchShop.Areas.Identity.Data;
using WatchShop.Enums;

namespace WatchShop.Models
{
    public class Watch
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Tên không được dài quá 50 ký tự")]
        [Display(Name = "Tên Sản Phẩm")]
        public string Name { get; set; }

        [Display(Name = "Giá Gốc")]
        public double Price { get; set; }

        [Range(1, 100, ErrorMessage = "Xin hãy nhập trong khoản 1 đến 100")]
        [Display(Name = "Giảm Giá %")]
        public int sale { get; set; }

        [Display(Name = "Giá sau khi giảm")]
        public double PriceAfterSale
        {
            get
            {
                double p = (100 - this.sale) / 100.0;
                double OriginPrice = (double)this.Price * p;
                return OriginPrice;
            }
        }

        
        [Display(Name = "Loại Kính")]
        public string GlassType { get; set; }

        
        [Display(Name = "Bộ Máy")]
        public string MachineType { get; set; }

        
        [Display(Name = "Loại dây")]
        public string WireMaterial { get; set; }

        
        [Display(Name = "Giới Tính")]
        public string Gender { get; set; }

        
        [Display(Name = "Độ Chống Nước")]
        public string WaterResistant { get; set; }

        
        [Display(Name = "Size Mặt")]
        public string FaceSize { get; set; }

        [Display(Name = "     ")]
        public string Pic1 { get; set; }

        [Display(Name = "Ảnh 2")]
        public string Pic2 { get; set; }

        [Display(Name = "Ảnh 3")]
        public string Pic3 { get; set; }

        [Display(Name = "Ảnh 4")]
        public string Pic4 { get; set; }

        [Display(Name = "số lượng sản phẩm ")]
        public int Stocking { get; set; }

        [Display(Name = "Hãng")]
        public int BrandId { get; set; }

        [Display(Name = "Hãng")]
        public virtual Brand Brand { get; set; }

        public bool isFavorite { get; set; } = false;


        public List<Order> Orders { get; set; }

        public List<Cart> Carts { get; } = [];

        public List<Favorite> Favorites { get; } = [];

        public List<WatchShopUser> Users { get; } = [];

        public List<HistoryPurchase> HistoryPurchases { get; } = [];
    }
}