using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using WatchShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Tracing;
using Microsoft.AspNetCore.Identity;
using WatchShop.Areas.Identity.Data;

namespace WatchShop.Pages.Shared.Components.ProductCard
{
    public class ProductCard : ViewComponent
    {

        private readonly UserManager<WatchShopUser> _userManager;

        private readonly WatchShop.Data.WatchShopContext _context;

        public ProductCard(WatchShop.Data.WatchShopContext context, UserManager<WatchShopUser> UserManager)
        {
            _context = context;
            _userManager = UserManager;
        }

        private Task<WatchShopUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IViewComponentResult> InvokeAsync(Watch watch)
        {
            


            return View(watch);
        }

        public async Task<IViewComponentResult> OnPostAddToCartAsync(int productId)
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return View();
            }

            var CurrentUser = await _context.Users.FindAsync(user.Id);

            if (CurrentUser != null)
            {
                var test = Activator.CreateInstance<Cart>();

                if (!OrderExists(CurrentUser.Id, productId))
                {
                    test.UserId = CurrentUser.Id;
                    test.WatchId = productId;
                    test.Quantity = 1;

                    _context.Cart.Add(test);
                    await _context.SaveChangesAsync();

                    return View("/Carts/index");
                }
                else
                {
                    var OrderAlreadyExist = GetOrder(CurrentUser.Id, productId);

                    if (OrderAlreadyExist != null)
                    {

                        OrderAlreadyExist.Quantity++;
                        _context.Attach(OrderAlreadyExist).State = EntityState.Modified;

                        try
                        {
                            await _context.SaveChangesAsync();

                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!OrderExists(CurrentUser.Id, productId))
                            {
                                return View();
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                }
            }
            return View("/Carts/index");
        }

        private bool OrderExists(string UserId, int watchId)
        {
            return _context.Cart.Any(x => x.UserId == UserId && x.WatchId == watchId);
        }

        private Cart GetOrder(string UserId, int watchId)
        {
            return _context.Cart.FirstOrDefault(x => x.UserId == UserId && x.WatchId == watchId);
        }
    }
}
