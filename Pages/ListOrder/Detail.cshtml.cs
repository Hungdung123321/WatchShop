using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WatchShop.Areas.Identity.Data;
using WatchShop.Models;

namespace WatchShop.Pages.ListOrder
{
    public class DetailModel : PageModel
    {
        private readonly UserManager<WatchShopUser> _userManager;

        private readonly WatchShop.Data.WatchShopContext _context;

        public DetailModel(UserManager<WatchShopUser> userManager, WatchShop.Data.WatchShopContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public Task<WatchShopUser> GetCurrentUserAsync() => _userManager.GetUserAsync(User);

        public List<Order> CurrentOrders { get; set; }

        public async Task<IActionResult> OnGetAsync(string? orderId)
        {
            var currentUser = await GetCurrentUserAsync();

            if (currentUser != null)
            {
                Guid currentOrderId = new Guid(orderId);
                IQueryable<Order> GetOrdersByUserId = from m in _context.Order
                                                      where m.OrderKey == currentOrderId
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
                                                          watch = m.watch,
                                                          WatchId = m.WatchId,
                                                      };

                
                CurrentOrders = await GetOrdersByUserId.ToListAsync();
            }

            return Page();
        }
        //ApproveOrder
        public async Task<IActionResult> OnPostApproveOrderAsync(string? orderId)
        {
            if (orderId != null)
            {   
                Guid NewOrderId = new Guid(orderId);
                List<Order> CurrentListOrder = _context.Order.Where(x=>x.OrderKey == NewOrderId).ToList();

                foreach (Order order in CurrentListOrder)
                {
                    order.Status = OrderStatus.Delivering;
                    _context.Update(order);
                }
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index",new { OrderId = orderId, Status= OrderStatus.Delivering });
            } 
            return Redirect("/Index");
        }
        //CancelOrder
        public async Task<IActionResult> OnPostCancelOrderAsync(string? orderId)
        {
            if (orderId != null)
            {
                Guid NewOrderId = new Guid(orderId);
                List<Order> CurrentListOrder = _context.Order.Where(x => x.OrderKey == NewOrderId).ToList();

                foreach (Order order in CurrentListOrder)
                {
                    order.Status = OrderStatus.Cancelled;
                    _context.Update(order);
                }
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index", new { OrderId = orderId, Status = OrderStatus.Cancelled });
            }
            return Redirect("/Index");
        }
        //PendingOrder
        public async Task<IActionResult> OnPostPendingOrderAsync(string? orderId)
        {
            if (orderId != null)
            {
                Guid NewOrderId = new Guid(orderId);
                List<Order> CurrentListOrder = _context.Order.Where(x => x.OrderKey == NewOrderId).ToList();

                foreach (Order order in CurrentListOrder)
                {
                    order.Status = OrderStatus.Pending;
                    _context.Update(order);
                }
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index", new { OrderId = orderId, Status = OrderStatus.Pending });
            }
            return Redirect("/Index");
        }
        //CompleteOrder
        public async Task<IActionResult> OnPostCompleteOrderAsync(string? orderId)
        {
            if (orderId != null)
            {
                Guid NewOrderId = new Guid(orderId);
                List<Order> CurrentListOrder = _context.Order.Where(x => x.OrderKey == NewOrderId).ToList();

                foreach (Order order in CurrentListOrder)
                {
                    order.Status = OrderStatus.Completed;
                    _context.Update(order);
                }
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index", new { OrderId = orderId, Status = OrderStatus.Completed });
            }
            return Redirect("/Index");
        }
    }
}
