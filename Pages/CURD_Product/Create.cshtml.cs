using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WatchShop.BaseClass;
using WatchShop.Data;
using WatchShop.Models;
using WatchShop.Validation;

namespace WatchShop.Pages.CURD_Product
{
    public class CreateModel : BrandNamePageModel
    {
        private readonly WatchShop.Data.WatchShopContext _context;

        public CreateModel(WatchShop.Data.WatchShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Watch Watch { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            PopulateMachineTypeSLDropDownList(_context);
            PopulateWireMaterialSLDropDownList(_context);
            PopulateWaterResistantSLDropDownList(_context);
            PopulateFaceSizeSLDropDownList(_context);
            PopulateGlassTypeSLDropDownList(_context);
            PopulateBrandDropDownList(_context);
            return Page();  
        }

        public async Task<IActionResult> OnPostAsync()
        {

            _context.Watch.Add(Watch);
            await _context.SaveChangesAsync();
            PopulateBrandDropDownList(_context);

            return RedirectToPage("./Index");
        }

        public string GetBrandLogoById(int id)
        {
            var brand = _context.Brand.Where(x => x.BrandId == id).FirstOrDefault();
            if (brand != null) { 
                return brand.LogoUrl;
            }
            else
            {
                return "";
            }
        }
    }
}
