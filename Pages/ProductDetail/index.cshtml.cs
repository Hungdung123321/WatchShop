using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using WatchShop.Areas.Identity.Data;
using WatchShop.Data;
using WatchShop.Models;

namespace WatchShop.Pages.ProductDetail
{
    public class indexModel : PageModel
    {

        private readonly UserManager<WatchShopUser> _userManager;

        private readonly WatchShop.Data.WatchShopContext _context;

        public indexModel(WatchShop.Data.WatchShopContext context, UserManager<WatchShopUser> UserManager)
        {
            _context = context;
            _userManager = UserManager;
        }

        [BindProperty(SupportsGet = true)]
        public int ProductID {set;get;}

        public Watch Watch { get; set; } = default!;

        public WatchShopUser user { get; set; } = default!;

        public Brand Brand { get; set; } = default!;


        private Task<WatchShopUser> GetCurrentUserAsync() => _userManager.GetUserAsync(User);

        public async Task<IActionResult> OnGetAsync()
        {
            if (ProductID == null)
            {
                return NotFound();
            }

            var watch = await _context.Watch.FindAsync(ProductID);

            if (watch == null)
            {
                return NotFound();
            }
            else
            {
                Watch = watch;
                Brand = await _context.Brand.FindAsync(Watch.BrandId);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCart1Async()
        {
            if (User.Identity.IsAuthenticated)
            {
                user = await GetCurrentUserAsync();

                if (user == null)
                {
                    return NotFound();
                }

                var CurrentUser = await _context.Users.FindAsync(user.Id);

                if (CurrentUser != null)
                {
                    var test = Activator.CreateInstance<Cart>();

                    if (!CartExists(CurrentUser.Id, ProductID))
                    {
                        test.UserId = CurrentUser.Id;
                        test.WatchId = ProductID;
                        test.Quantity = 1;

                        _context.Cart.Add(test);
                        await _context.SaveChangesAsync();

                        return Redirect("/Payment/index");
                    }
                    else
                    {
                        var CartAlreadyExist = GetCart(CurrentUser.Id, ProductID);

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
                                if (!CartExists(CurrentUser.Id, ProductID))
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
                return Redirect("/Payment/index");
            }
            return Redirect("/Identity/Account/Login");
        }

        public async Task<IActionResult> OnPostAddToCart2Async()
        {
            if (User.Identity.IsAuthenticated)
            {
                user = await GetCurrentUserAsync();

                if (user == null)
                {
                    return NotFound();
                }

                var CurrentUser = await _context.Users.FindAsync(user.Id);

                if (CurrentUser != null)
                {
                    var test = Activator.CreateInstance<Cart>();

                    if (!CartExists(CurrentUser.Id, ProductID))
                    {
                        test.UserId = CurrentUser.Id;
                        test.WatchId = ProductID;
                        test.Quantity = 1;

                        _context.Cart.Add(test);
                        await _context.SaveChangesAsync();

                        return Redirect("/Carts/index");
                    }
                    else
                    {
                        var CartAlreadyExist = GetCart(CurrentUser.Id, ProductID);

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
                                if (!CartExists(CurrentUser.Id, ProductID))
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

        private bool CartExists(string UserId,int watchId)
        {
            return _context.Cart.Any(x => x.UserId == UserId && x.WatchId == watchId);
        }

        private Cart GetCart(string UserId, int watchId)
        {
            return _context.Cart.FirstOrDefault(x => x.UserId == UserId && x.WatchId == watchId);
        }
    }
}
