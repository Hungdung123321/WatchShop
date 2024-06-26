using Azure;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using WatchShop.Areas.Identity.Data;

namespace WatchShop.Models
{
    public class Cart
    {
        public int Id { get; set; } 

        public int Quantity { get; set; } = 0;

        public string UserId { get; set; }  

        public int WatchId { get; set; } 

        public  WatchShopUser User { get; set; } = null!;

        [ForeignKey("WatchId")]
        public  Watch Watch { get; set; } = null!;

    }
    
    
}
