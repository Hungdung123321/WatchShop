using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WatchShop.Areas.Identity.Data;
using WatchShop.Models;

namespace WatchShop.Pages.profiles
{
    [Authorize]
    public class IndexModel : PageModel
    {

        private readonly UserManager<WatchShopUser> _userManager;

        private readonly WatchShop.Data.WatchShopContext _context;

        public IndexModel(UserManager<WatchShopUser> userManager, WatchShop.Data.WatchShopContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public WatchShopUser? appUser;

        //public Task<WatchShopUser> GetCurrentUserAsync() => _userManager.GetUserAsync(User);

        public List<Order> CurentOrdersByUserId {  get; set; }
        
        [BindProperty(SupportsGet = true)]
        public OrderStatus Status {  get; set; } = OrderStatus.Pending;

        public async Task<IActionResult> OnGetAsync()
        {
            var task = _userManager.GetUserAsync(User);
            task.Wait();
            appUser = task.Result;
            //var currentUser = await GetCurrentUserAsync();
            var ListOrderKey = _context.Order.Select(x => x.OrderKey).Distinct();

            IQueryable<Order> GetOrdersByUserId = from x in ListOrderKey
                                                  from m in _context.Order.Where(o => o.OrderKey == x).Take(1)
                                                  select new Order
                                                  {
                                                      Id = m.Id,
                                                      OrderKey = m.OrderKey,
                                                      Address = m.Address,
                                                      City = m.City,
                                                      CreatedDate = m.CreatedDate,
                                                      PhoneNumber = m.PhoneNumber,
                                                      Status = m.Status,
                                                      User = m.User,
                                                      UserId = m.UserId,
                                                      watch = m.watch,
                                                      WatchId = m.WatchId,
                                                  };

            if (appUser != null)
            {
                GetOrdersByUserId = GetOrdersByUserId.Where(x => x.UserId == appUser.Id);
                GetOrdersByUserId = GetOrdersByUserId.Where(x => x.Status == Status);
                CurentOrdersByUserId = await GetOrdersByUserId.ToListAsync();
            }

            return Page();
        }

        public List<Order> GetOrderWatchsById (Guid orderId)
        {
            IQueryable<Order> GetOrdersById= from m in _context.Order
                                                  where m.OrderKey == orderId
                                                  select new Order
                                                  {
                                                      Id = m.Id,
                                                      OrderKey = m.OrderKey,
                                                      Address = m.Address,
                                                      City = m.City,
                                                      CreatedDate = m.CreatedDate,
                                                      PhoneNumber = m.PhoneNumber,
                                                      Status = m.Status,
                                                      Qty = m.Qty,
                                                      User = m.User,
                                                      UserId = m.UserId,
                                                      watch = new Watch
                                                      {
                                                          Id=m.watch.Id,
                                                          Name=m.watch.Name,
                                                          Pic1 = m.watch.Pic1,  
                                                          Brand = m.watch.Brand,
                                                          Price = m.watch.PriceAfterSale,
                                                      },
                                                      WatchId = m.WatchId,
                                                  };
            return GetOrdersById.ToList();
        }

    }
}
