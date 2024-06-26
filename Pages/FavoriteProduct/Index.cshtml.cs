using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WatchShop.Areas.Identity.Data;
using WatchShop.Models;

namespace WatchShop.Pages.FavoriteProduct
{
    [Authorize]
    public class IndexModel : PageModel
    {

        private readonly UserManager<WatchShopUser> _userManager;

        private readonly WatchShop.Data.WatchShopContext _context;

        public IList<Favorite> Favorites;

        public IndexModel(UserManager<WatchShopUser> userManager, WatchShop.Data.WatchShopContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        private Task<WatchShopUser> GetCurrentUserAsync() => _userManager.GetUserAsync(User);

        private IQueryable<Favorite> GetListFavoritebyUser(string UserId)
        {
            var result = from s in _context.Favorite
                         where s.UserId == UserId
                         select new Favorite
                         {
                             Id = s.Id,
                             UserId = s.UserId,
                             WatchId = s.WatchId,
                             Watch = new Watch()
                             {
                                 Id = s.WatchId,
                                 Brand = s.Watch.Brand,
                                 Name = s.Watch.Name,
                                 Pic1 = s.Watch.Pic1,
                             },
                         };

            return result;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return NotFound();
            }

            var CurrentUser = await _context.Users.FindAsync(user.Id);

            if (CurrentUser != null)
            {
                Favorites = await GetListFavoritebyUser(CurrentUser.Id).AsNoTracking().ToListAsync();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToFavoriteAsync(int productId)
        {

            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return NotFound();
            }

            var CurrentFavorite = GetFavorite(user.Id, productId);

            if (CurrentFavorite != null)
            {
                _context.Favorite.Remove(CurrentFavorite);
                await _context.SaveChangesAsync();
            }

            return Redirect("/FavoriteProduct/index");
        }

        private Favorite GetFavorite(string UserId, int watchId)
        {
            return _context.Favorite.FirstOrDefault(x => x.UserId == UserId && x.WatchId == watchId);
        }
    }
}
