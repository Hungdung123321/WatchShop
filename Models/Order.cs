using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WatchShop.Areas.Identity.Data;

namespace WatchShop.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid OrderKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.None;

        [Display(Name = "Thành Phố")]
        public string City { get; set; }
        [Display(Name = "Địa Chỉ")]
        public string Address { get; set; }
        
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        public int Qty { get; set; } = 0;


        public string UserId { get; set; }
        public int WatchId { get; set; }

        public WatchShopUser User { get; set; } = null!;

        public Watch watch { get; set; }
    }


    public enum OrderStatus
    {
        None,
        Pending,
        Delivering,
        Completed,
        Cancelled,

    }
}
