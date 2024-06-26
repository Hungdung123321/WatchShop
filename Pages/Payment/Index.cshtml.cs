using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WatchShop.Areas.Identity.Data;
using WatchShop.Models;

namespace WatchShop.Pages.Payment
{
    public class IndexModel : PageModel
    {

        private readonly UserManager<WatchShopUser> _userManager;

        private readonly WatchShop.Data.WatchShopContext _context;

        public IndexModel(UserManager<WatchShopUser> userManager, WatchShop.Data.WatchShopContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public Task<WatchShopUser> GetCurrentUserAsync() => _userManager.GetUserAsync(User);

        public List<Cart> CurrentCarts { get; set; }

        [BindProperty]
        public Order OrderInput { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = await GetCurrentUserAsync();

            if (currentUser != null) {
                IQueryable<Cart> GetCartsByUserId = from m in _context.Cart
                                               where m.UserId == currentUser.Id
                                               select new Cart
                                               {
                                                   Watch = m.Watch,
                                                   Quantity = m.Quantity,
                                               };

                

                CurrentCarts = await GetCartsByUserId.ToListAsync();
                
            }
            return Page();
        }

        public async Task<IActionResult> OnPostPayAsync()
        {

            var currentUser = await GetCurrentUserAsync();

            if (currentUser != null)
            {
                IQueryable<Cart> GetOrdersByUserId = from m in _context.Cart
                                                     where m.UserId == currentUser.Id
                                                     select m;

                CurrentCarts = await GetOrdersByUserId.ToListAsync();
                try
                {
                    Guid newOrderKey = Guid.NewGuid();
                    DateTime nowDate = DateTime.Now;
                    List<Order> NewOrders = new List<Order>();
                    foreach (var cart in CurrentCarts) {
                        Order NewOrder = new Order
                        {
                            OrderKey = newOrderKey,
                            CreatedDate = nowDate,
                            Status = OrderStatus.Pending,
                            City = OrderInput.City,
                            Address = OrderInput.Address,
                            PhoneNumber = OrderInput.PhoneNumber,
                            Qty = cart.Quantity,
                            User = currentUser,
                            UserId = currentUser.Id,
                            watch = cart.Watch,
                            WatchId = cart.WatchId,
                        };
                        NewOrders.Add(NewOrder);
                    }
                        
                    _context.Order.AddRange(NewOrders);
                    _context.Cart.RemoveRange(CurrentCarts);
                    await _context.SaveChangesAsync();
                    return Redirect("/index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }
            return Redirect("/index");
        }
    }
}
