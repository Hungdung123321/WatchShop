using System.ComponentModel.DataAnnotations.Schema;
using WatchShop.Areas.Identity.Data;

namespace WatchShop.Models
{
    public class HistoryPurchase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid HistoryPurchaseKey { get; set; }

        public DateTime DatePurchase { get; set; }

        public string UserId { get; set; }

        public int WatchId { get; set; }

        public WatchShopUser User { get; set; } = null!;

        public Watch Watch { get; set; } = null!;

    }
}
