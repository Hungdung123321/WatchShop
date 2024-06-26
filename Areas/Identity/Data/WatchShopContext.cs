using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using WatchShop.Areas.Identity.Data;
using WatchShop.Models;

namespace WatchShop.Data;

public class WatchShopContext : IdentityDbContext<WatchShopUser>
{
    public WatchShopContext(DbContextOptions<WatchShopContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //var client = new IdentityRole("client");
        //client.NormalizedName = "client";

        //var admin = new IdentityRole("admin");
        //admin.NormalizedName = "admin";

        //var seller = new IdentityRole("seller");
        //seller.NormalizedName = "seller";

        //builder.Entity<IdentityRole>().HasData(client,admin,seller);

        //builder.Entity<Order>()
        //.HasMany(e => e.CartItems)
        //.WithOne(e => e.Order)
        //.HasForeignKey(e => e.OrderId)
        //.IsRequired(false);

    }

    public DbSet<WatchShop.Models.Watch> Watch { get; set; } = default!;
    public DbSet<WatchShop.Models.Cart> Cart { get; set; } = default!;
    public DbSet<WatchShop.Models.Order> Order { get; set; } = default!;

    public DbSet<WatchShop.Models.Brand> Brand { get; set; } = default!;
    public DbSet<WatchShop.Models.Favorite> Favorite { get; set; } = default!;
    public DbSet<WatchShop.Models.HistoryPurchase> HistoryPurchase { get; set; } = default!;
}
