using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WatchShop.Models;

namespace WatchShop.Areas.Identity.Data;

// Add profile data for application users by adding properties to the WatchShopUser class
public class WatchShopUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; }

    public List<Cart> Carts { get; set; }

    public List<Order> Bills { get; set; }

    public List<Favorite> Favorites { get; } = [];

    public List<Watch> Watchs { get; } = [];

    public List<HistoryPurchase> HistoryPurchases { get; } = [];
}

