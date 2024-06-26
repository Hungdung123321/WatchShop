using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WatchShop.Data;
using WatchShop.Models;

namespace WatchShop.Pages.CURD_Brand
{
    public class IndexModel : PageModel
    {
        private readonly WatchShop.Data.WatchShopContext _context;

        public IndexModel(WatchShop.Data.WatchShopContext context)
        {
            _context = context;
        }

        public IList<Brand> Brand { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Brand = await _context.Brand.ToListAsync();
        }
    }
}
