using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WatchShop.Data;
using WatchShop.Models;

namespace WatchShop.Pages.CURD_Product
{
    public class DetailsModel : PageModel
    {
        private readonly WatchShop.Data.WatchShopContext _context;

        public DetailsModel(WatchShop.Data.WatchShopContext context)
        {
            _context = context;
        }

        public Watch Watch { get; set; } = default!;

        public Brand Brand { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watch = await _context.Watch.FirstOrDefaultAsync(m => m.Id == id);
            

            if (watch == null)
            {
                return NotFound();
            }
            else
            {
                Watch = watch;
                Brand = await _context.Brand.FindAsync(Watch.BrandId);
            }
            return Page();
        }
    }
}
