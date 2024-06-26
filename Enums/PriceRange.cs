using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WatchShop.Enums
{
    public enum PriceRange
    {
        
        [Display(Name = "Tất cả")]
        PriceRangeAll,
        [Display(Name = "Dưới 2 triệu")]
        PriceRangeLv1,
        [Display(Name = "Từ 2 - 5 triệu")]
        PriceRangeLv2,
        [Display(Name = "Từ 5 - 10 triệu")]
        PriceRangeLv3,
        [Display(Name = "Từ 10 - 20 triệu")]
        PriceRangeLv4,
        [Display(Name = "Từ 20 - 50 triệu")]
        PriceRangeLv5,
        [Display(Name = "Trên 50 Triệu")]
        PriceRangeLv6,
    }

}
