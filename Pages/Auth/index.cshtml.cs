using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WatchShop.Enums;
using WatchShop.Models;
using WatchShop.Utils;

namespace WatchShop.Pages.Auth
{
    public class indexModel : PageModel
    {
        

        [BindProperty(SupportsGet = true)]
        public AuthPage PageMode {set;get;}

        

        public void OnGetAsync()
        {
           
        }
    }
}
