using System.ComponentModel.DataAnnotations;

namespace WatchShop.Models
{
    public class Brand
    {
        public int BrandId { get; set; }

        [Display(Name ="Tên")]
        public string Name { get; set; }

        [Display(Name ="Chi tiết Hãng")]
        public string Description { get; set; }
        [Display(Name ="Logo")]
        public string LogoUrl { get; set; }
        [Display(Name = "xuất sứ")]
        public string Origin { get; set; }
    }
}
