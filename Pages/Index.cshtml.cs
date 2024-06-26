using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WatchShop.Areas.Identity.Data;
using WatchShop.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WatchShop.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    private readonly UserManager<WatchShopUser> _userManager;

    private readonly WatchShop.Data.WatchShopContext _context;

    public IList<Watch> Watchs { get; set; } = default!;

    public IList<Brand> Brands { get; set; } = default!;

    public IndexModel(ILogger<IndexModel> logger, Data.WatchShopContext context, UserManager<WatchShopUser> UserManager)
    {
        _logger = logger;
        _context = context;
        _userManager = UserManager;
    }

    public Task<WatchShopUser> GetCurrentUserAsync() => _userManager.GetUserAsync(User);

    public WatchShopUser currentUser { get; set; }  

    public async Task OnGetAsync()
    {
        currentUser = await GetCurrentUserAsync();

        Watchs = await _context.Watch.ToListAsync();
        Brands = await _context.Brand.ToListAsync();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(int productId)
    {
        if (User.Identity.IsAuthenticated) {
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

                if (!FavoriteExists(CurrentUser.Id, productId)) {

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

    private bool CartExists(string UserId, int watchId)
    {
        return _context.Cart.Any(x => x.UserId == UserId && x.WatchId == watchId);
    }

    private Cart GetCart(string UserId, int watchId)
    {
        return _context.Cart.FirstOrDefault(x => x.UserId == UserId && x.WatchId == watchId);
    }

    public bool FavoriteExists(string UserId, int watchId)
    {
        return _context.Favorite.Any(x => x.UserId == UserId && x.WatchId == watchId);
    }

    private Favorite GetFavorite(string UserId, int watchId)
    {
        return _context.Favorite.FirstOrDefault(x => x.UserId == UserId && x.WatchId == watchId);
    }

}
