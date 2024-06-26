using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WatchShop.Areas.Identity.Data;
using WatchShop.Models;

namespace WatchShop.Pages.ManageUser
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {

        private readonly UserManager<WatchShopUser> _userManager;

        private readonly WatchShop.Data.WatchShopContext _context;

        

        public IndexModel(UserManager<WatchShopUser> userManager, WatchShop.Data.WatchShopContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public class UserRolesViewModel
        {
            [Display(Name ="Mã người dùng")]
            public string Id { get; set; }
            [Display(Name = "Tên người dùng")]
            public string UserName { get; set; }
            [Display(Name = "Email")]
            public string Email { get; set; }
            [Display(Name = "Số điện thoại")]
            public string PhoneNumber { get; set; }
            [Display(Name = "Role")]
            public IEnumerable<string> Roles { get; set; }

            public WatchShopUser UserData { get; set; }
        }

        public List<UserRolesViewModel> ListUserModel { get; set; } = new List<UserRolesViewModel>();

        [BindProperty(SupportsGet = true)]
        public string role { get; set; } = "";

        [BindProperty(SupportsGet = true)]
        public string SearchString {  get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            foreach (WatchShopUser user in users)
            {
                UserRolesViewModel urv = new UserRolesViewModel()
                {
                    Id = user.Id,
                    UserName = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Roles = await _userManager.GetRolesAsync(user),
                    UserData = user
                };
                ListUserModel.Add(urv);
            }

            if (!string.IsNullOrEmpty(role))
            {
                ListUserModel = ListUserModel.Where(x => x.Roles.Contains(role)).ToList();
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                ListUserModel = ListUserModel.Where(x => x.UserName.Contains(SearchString)).ToList();
            }

            ListUserModel = ListUserModel.ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostChangeToAdminAsync(string UserID)
        {

            var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.Id == UserID);

            if (user == null || ListUserModel == null)
            {
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            if (currentRoles != null)
            {
                if (currentRoles.Contains("client"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "client");
                    await _userManager.AddToRoleAsync(user, "admin");
                }
                else if (currentRoles.Contains("seller"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "seller");
                    await _userManager.AddToRoleAsync(user, "admin");
                }
                _context.Entry(user).State = EntityState.Modified;
                return Redirect("/ManageUser/index");
            }

            return Redirect("/index");
        }

        public async Task<IActionResult> OnPostChangeToClientAsync(string UserID)
        {

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == UserID);

            if (user == null || ListUserModel == null)
            {
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            if (currentRoles != null)
            {
                if (currentRoles.Contains("admin"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "admin");
                    await _userManager.AddToRoleAsync(user, "client");
                }
                else if (currentRoles.Contains("seller"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "seller");
                    await _userManager.AddToRoleAsync(user, "client");
                }
                _context.Entry(user).State = EntityState.Modified;
                return Redirect("/ManageUser/index");
            }

            return Redirect("/index");
        }

        public async Task<IActionResult> OnPostChangeToSellerAsync(string UserID)
        {

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == UserID);

            if (user == null || ListUserModel == null)
            {
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            if (currentRoles != null)
            {
                if (currentRoles.Contains("client"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "client");
                    await _userManager.AddToRoleAsync(user, "seller");
                }
                else if (currentRoles.Contains("admin"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "admin");
                    await _userManager.AddToRoleAsync(user, "seller");
                }
                _context.Entry(user).State = EntityState.Modified;
                return Redirect("/ManageUser/index");
            }

            return Redirect("/index");
        }
    }
}
