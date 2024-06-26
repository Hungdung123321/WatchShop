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
    public class DeleteModel : PageModel
    {
        private readonly WatchShop.Data.WatchShopContext _context;

        public DeleteModel(WatchShop.Data.WatchShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Watch Watch { get; set; } = default!;

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
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watch = await _context.Watch.FindAsync(id);
            if (watch != null)
            {
                Watch = watch;
                _context.Watch.Remove(Watch);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
