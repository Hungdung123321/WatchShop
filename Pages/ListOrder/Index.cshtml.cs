using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Linq.Expressions;
using WatchShop.Areas.Identity.Data;
using WatchShop.Models;

namespace WatchShop.Pages.ListOrder
{
    [Authorize(Roles = "admin,seller")]
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

        public List<Order> Orders { get; set; }

        [BindProperty(SupportsGet = true)]
        public string OrderId { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public async Task<IActionResult> OnGetAsync(string? orderId)
        {
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


            if (!string.IsNullOrEmpty(orderId))
            {
                Guid SearchOrderId = new Guid(orderId);
                GetOrdersByUserId = GetOrdersByUserId.Where(x => x.OrderKey == SearchOrderId);
            }
            else if(!string.IsNullOrEmpty(OrderId)){
                Guid SearchOrderId = new Guid(OrderId);
                GetOrdersByUserId = GetOrdersByUserId.Where(x => x.OrderKey == SearchOrderId);
            }

            GetOrdersByUserId = GetOrdersByUserId.Where(x => x.Status == Status);

            Orders = GetOrdersByUserId.OrderByDescending(x => x.CreatedDate).ToList();
            return Page();
        }
        

    }
}
