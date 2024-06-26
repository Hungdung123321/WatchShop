using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using WatchShop.Areas.Identity.Data;
using WatchShop.BaseClass;
using WatchShop.Enums;
using WatchShop.Models;
using WatchShop.Utils;

namespace WatchShop.Pages.ListProduct
{
    public class IndexModel : BrandNamePageModel
    {
        private readonly WatchShop.Data.WatchShopContext _context;

        private readonly UserManager<WatchShopUser> _userManager;

        public IndexModel(WatchShop.Data.WatchShopContext context, UserManager<WatchShopUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty(SupportsGet = true)]
        public int BrandId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string GlassType { get; set; }

        [BindProperty(SupportsGet = true)]
        public string MachineType { get; set; }

        [BindProperty(SupportsGet = true)]
        public string WireMaterial { get; set; }

        [BindProperty(SupportsGet = true)]
        public string WaterResistant { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FaceSize { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Gender { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PriceRange { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public IList<Watch> Watchs { get; set; } = default!;

        public Task<WatchShopUser> GetCurrentUserAsync() => _userManager.GetUserAsync(User);

        public WatchShopUser currentUser { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            currentUser = await GetCurrentUserAsync();

            IQueryable<Watch> WatchQuery = from m in _context.Watch
                                           orderby m
                                           select m;

            PopulateMachineTypeSLDropDownList(_context);
            PopulateWireMaterialSLDropDownList(_context);
            PopulateWaterResistantSLDropDownList(_context);
            PopulateFaceSizeSLDropDownList(_context);
            PopulateGlassTypeSLDropDownList(_context);
            PopulateBrandDropDownList(_context);


            if (!string.IsNullOrEmpty(SearchString))
            {
                WatchQuery = WatchQuery.Where(s => s.Name.Contains(SearchString));
            }

            if (BrandId != -1)
            {
                WatchQuery = WatchQuery.Where(s => s.BrandId == BrandId);
            }

            if (!string.IsNullOrEmpty(GlassType))
            {
                WatchQuery = WatchQuery.Where(s => s.GlassType == GlassType);
            }

            if (!string.IsNullOrEmpty(MachineType))
            {
                WatchQuery = WatchQuery.Where(s => s.MachineType == MachineType);
            }

            if (!string.IsNullOrEmpty(WireMaterial))
            {
                WatchQuery = WatchQuery.Where(s => s.WireMaterial == WireMaterial);
            }

            if (!string.IsNullOrEmpty(WaterResistant))
            {
                WatchQuery = WatchQuery.Where(s => s.WaterResistant == WaterResistant);
            }

            if (!string.IsNullOrEmpty(FaceSize))
            {
                WatchQuery = WatchQuery.Where(s => s.FaceSize == FaceSize);
            }

            if (!string.IsNullOrEmpty(Gender))
            {
                WatchQuery = WatchQuery.Where(s => s.Gender == Gender);
            }

            if (PriceRange != 0)
            {
                switch (PriceRange) {
                    case 1:
                        WatchQuery = WatchQuery.Where(s => (double)s.Price <= 2000000);
                        break;
                    case 2:
                        WatchQuery = WatchQuery.Where(s => (double)s.Price >= 2000000 && (double)s.Price <= 5000000);
                        break;
                    case 3:
                        WatchQuery = WatchQuery.Where(s => (double)s.Price >= 5000000 && (double)s.Price <= 10000000);
                        break;
                    case 4:
                        WatchQuery = WatchQuery.Where(s => (double)s.Price >= 10000000 && (double)s.Price <= 20000000);
                        break;
                    case 5:
                        WatchQuery = WatchQuery.Where(s => (double)s.Price >= 20000000 && (double)s.Price <= 50000000);
                        break;
                    case 6:
                        WatchQuery = WatchQuery.Where(s => (double)s.Price >= 50000000);
                        break;
                }
            }

            Watchs = await WatchQuery.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAddToFavoriteAsync(int productId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await GetCurrentUserAsync();

                if (user == null)
                {
                    return NotFound();
                }

                var CurrentUser = await _context.Users.FindAsync(user.Id);

                if (CurrentUser != null)
                {
                    var favorite = Activator.CreateInstance<Favorite>();

                    if (!FavoriteExists(CurrentUser.Id, productId))
                    {

                        favorite.UserId = CurrentUser.Id;
                        favorite.WatchId = productId;

                        _context.Favorite.Add(favorite);
                        await _context.SaveChangesAsync();

                        return Redirect("/FavoriteProduct/index");
                    }
                    else
                    {
                        var CurrentFavorite = GetFavorite(user.Id, productId);

                        if (CurrentFavorite != null)
                        {
                            _context.Favorite.Remove(CurrentFavorite);
                            await _context.SaveChangesAsync();
                        }

                        return Redirect("/FavoriteProduct/index");
                    }

                }
            }

            return Redirect("/Identity/Account/Login");
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int productId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await GetCurrentUserAsync();

                if (user == null)
                {
                    return NotFound();
                }

                var CurrentUser = await _context.Users.FindAsync(user.Id);

                if (CurrentUser != null)
                {
                    var test = Activator.CreateInstance<Cart>();

                    if (!CartExists(CurrentUser.Id, productId))
                    {
                        test.UserId = CurrentUser.Id;
                        test.WatchId = productId;
                        test.Quantity = 1;

                        _context.Cart.Add(test);
                        await _context.SaveChangesAsync();

                        return Redirect("/Carts/index");
                    }
                    else
                    {
                        var CartAlreadyExist = GetCart(CurrentUser.Id, productId);

                        if (CartAlreadyExist != null)
                        {

                            CartAlreadyExist.Quantity++;
                            _context.Attach(CartAlreadyExist).State = EntityState.Modified;

                            try
                            {
                                await _context.SaveChangesAsync();

                            }
                            catch (DbUpdateConcurrencyException)
                            {
                                if (!CartExists(CurrentUser.Id, productId))
                                {
                                    return NotFound();
                                }
                                else
                                {
                                    throw;
                                }
                            }
                        }
                    }
                }
                return Redirect("/Carts/index");
            }
            return Redirect("/Identity/Account/Login");
        }

        public bool FavoriteExists(string UserId, int watchId)
        {
            return _context.Favorite.Any(x => x.UserId == UserId && x.WatchId == watchId);
        }

        private Favorite GetFavorite(string UserId, int watchId)
        {
            return _context.Favorite.FirstOrDefault(x => x.UserId == UserId && x.WatchId == watchId);
        }

        private bool CartExists(string UserId, int watchId)
        {
            return _context.Cart.Any(x => x.UserId == UserId && x.WatchId == watchId);
        }

        private Cart GetCart(string UserId, int watchId)
        {
            return _context.Cart.FirstOrDefault(x => x.UserId == UserId && x.WatchId == watchId);
        }
    }
}
