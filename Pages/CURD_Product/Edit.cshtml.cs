using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WatchShop.BaseClass;
using WatchShop.Data;
using WatchShop.Enums;
using WatchShop.Models;

namespace WatchShop.Pages.CURD_Product
{
    public class EditModel : BrandNamePageModel
    {
        private readonly WatchShop.Data.WatchShopContext _context;

        public EditModel(WatchShop.Data.WatchShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Watch Watch { get; set; } = default!;

        public IList<Brand> Brands { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Brands = await _context.Brand.ToListAsync();

            PopulateMachineTypeSLDropDownList(_context);
            PopulateWireMaterialSLDropDownList(_context);
            PopulateWaterResistantSLDropDownList(_context);
            PopulateFaceSizeSLDropDownList(_context);
            PopulateGlassTypeSLDropDownList(_context);
            PopulateBrandDropDownList(_context);

            var watch =  await _context.Watch.FirstOrDefaultAsync(m => m.Id == id);

            if (watch == null)
            {
                return NotFound();
            }
            Watch = watch;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Attach(Watch).State = EntityState.Modified;
            PopulateBrandDropDownList(_context);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WatchExists(Watch.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool WatchExists(int id)
        {
            return _context.Watch.Any(e => e.Id == id);
        }
    }
}
