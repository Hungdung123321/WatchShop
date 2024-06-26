using WatchShop.Areas.Identity.Data;

namespace WatchShop.Models
{
    public class Favorite
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int WatchId { get; set; }

        public WatchShopUser User { get; set; } = null!;

        public Watch Watch { get; set; } = null!;
    }
}
