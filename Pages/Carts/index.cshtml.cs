using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using WatchShop.Areas.Identity.Data;
using WatchShop.Data;
using WatchShop.Models;

namespace WatchShop.Pages.Carts
{
    [Authorize]
    public class indexModel : PageModel
    {

        private readonly UserManager<WatchShopUser> _userManager;

        private readonly WatchShop.Data.WatchShopContext _context;

        public WatchShopUser? appUser;

        

        public double TotalPrice { get; set; } = 0;

        public indexModel(UserManager<WatchShopUser> userManager, WatchShop.Data.WatchShopContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        private Task<WatchShopUser> GetCurrentUserAsync() => _userManager.GetUserAsync(User);

        public enum PaymentMethod{
            Cod,
            Transfer
        }

        public List<Cart> Carts { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var task = _userManager.GetUserAsync(User);
            task.Wait();
            appUser = task.Result;

            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return NotFound();
            }

            var CurrentUser = await _context.Users.FindAsync(user.Id);

            if (CurrentUser != null) {
                Carts = await GetListCartbyUser(CurrentUser.Id).AsNoTracking().ToListAsync();
                foreach (Cart item in Carts)
                {
                    TotalPrice += item.Watch.PriceAfterSale * item.Quantity;
                }
            }

            return Page();
        }

        

        public async Task<IActionResult> OnPostAddQuantityAsync(int productId)
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return NotFound();
            }

            var CurrentCart = GetCart(user.Id, productId); 

            if (CurrentCart != null)
            {
                CurrentCart.Quantity++;
                _context.Attach(CurrentCart).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();

                    return Redirect("/Carts/index");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(CurrentCart.UserId, CurrentCart.WatchId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }


            return Redirect("/Carts/index");
        }

        public async Task<IActionResult> OnPostRemoveQuantityAsync(int productId)
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return NotFound();
            }
            
            var CurrentCart = GetCart(user.Id, productId);

            if (CurrentCart != null && CurrentCart.Quantity != 1)
            {
                CurrentCart.Quantity--;
                _context.Attach(CurrentCart).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();

                    return Redirect("/Carts/index");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(CurrentCart.UserId, CurrentCart.WatchId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }


            return Redirect("/Carts/index");
        }

        public async Task<IActionResult> OnPostRemoveCartItemAsync(int productId)
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return NotFound();
            }

            var CurrentCart = GetCart(user.Id, productId);

            if (CurrentCart != null)
            {
                _context.Cart.Remove(CurrentCart);
                await _context.SaveChangesAsync();
            }

            return Redirect("/Carts/index");
        }

        private IQueryable<Cart> GetListCartbyUser(string UserId)
        {
            var result = from s in _context.Cart
                         where s.UserId == UserId
                         select new Cart
                         {
                             Id = s.Id,
                             Quantity = s.Quantity,
                             UserId = s.UserId,
                             WatchId = s.WatchId,
                             Watch = new Watch()
                             {
                                 Id = s.WatchId,
                                 Brand = s.Watch.Brand,
                                 Name = s.Watch.Name,
                                 Pic1 = s.Watch.Pic1,
                                 Price = s.Watch.PriceAfterSale,
                                
                             },
                         };

            return result;
        }

        
        //public async Task<IActionResult> OnPostPayAsync()
        //{
        //    var user = await GetCurrentUserAsync();

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var CurrentCarts = await GetListCartbyUser(user.Id).AsNoTracking().ToListAsync();

        //    if (CurrentCarts != null) {
        //        var newKey = Guid.NewGuid();
        //        IList<HistoryPurchase> Purchases = new List<HistoryPurchase>();

        //        foreach (var order in CurrentCarts) {
        //            Purchases.Add(new HistoryPurchase()
        //            {
        //                UserId = user.Id,
        //                WatchId= order.WatchId,
        //                HistoryPurchaseKey = newKey,
        //                DatePurchase = DateTime.Now,
        //            });
        //            ModelState.Clear();
        //        }

        //        if(Purchases != null)
        //        {
        //            _context.HistoryPurchase.AddRange(Purchases);
        //            await _context.SaveChangesAsync();
                    
        //            return Redirect("/Carts/index");
        //        }

        //    }

        //    return Redirect("/index");
        //}
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
