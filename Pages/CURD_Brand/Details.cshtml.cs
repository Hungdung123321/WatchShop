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
    public class DetailsModel : PageModel
    {
        private readonly WatchShop.Data.WatchShopContext _context;

        public DetailsModel(WatchShop.Data.WatchShopContext context)
        {
            _context = context;
        }

        public Brand Brand { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brand.FirstOrDefaultAsync(m => m.BrandId == id);
            if (brand == null)
            {
                return NotFound();
            }
            else
            {
                Brand = brand;
            }
            return Page();
        }
    }
}
