using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WatchShop.Data;
using WatchShop.Areas.Identity.Data;
using WatchShop.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("WatchShopContextConnection") ?? throw new InvalidOperationException("Connection string 'WatchShopContextConnection' not found.");
var config = builder.Configuration;


builder.Services.AddDbContext<WatchShopContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<WatchShopUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<WatchShopContext>();

var configuration = builder.Configuration;
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    IConfigurationSection googleAuthNSection =
       config.GetSection("Authentication:Google");
    googleOptions.ClientId = "460530704203-ajm3htf2duvkajkgts19c372ucjlqfgp.apps.googleusercontent.com";
    googleOptions.ClientSecret = "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";
});

builder.Services.AddRazorPages();



builder.Services.Configure<IdentityOptions>(option =>
{
    
});

var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedUser.InitializeAsync(services);
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();   
app.UseAuthorization();

app.MapRazorPages();


app.Run();
