using WatchShop.Data;
using WatchShop.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WatchShop.Enums;

namespace WatchShop.BaseClass
{
    public class BrandNamePageModel : PageModel
    {
        public SelectList BrandNameSL { get; set; }
        public SelectList GlassTypeSL { get; set; }
        public SelectList MachineTypeSL { get; set; }
        public SelectList WireMaterialSL { get; set; }
        public SelectList WaterResistantSL { get; set; }
        public SelectList FaceSizeSL { get; set; }

        public void PopulateBrandDropDownList(WatchShopContext _context,
            object selectedDepartment = null)
        {
            var departmentsQuery = from d in _context.Brand
                                   orderby d.Name // Sort by name.
                                   select d;

            BrandNameSL = new SelectList(departmentsQuery.AsNoTracking(),
                nameof(Brand.BrandId),
                nameof(Brand.Name),
                selectedDepartment);
        }

        public void PopulateGlassTypeSLDropDownList(WatchShopContext _context,
            object selectedDepartment = null)
        {
            var departmentsQuery = from d in _context.Watch
                                   orderby d.GlassType // Sort by name.
                                   select d.GlassType;

            var glassTypeList = departmentsQuery.Distinct().ToList();

            GlassTypeSL = new SelectList(glassTypeList);
        }

        public void PopulateMachineTypeSLDropDownList(WatchShopContext _context,
           object selectedDepartment = null)
        {
            var departmentsQuery = from d in _context.Watch
                                   orderby d.MachineType // Sort by name.
                                   select d.MachineType;

            var MachineTypeList = departmentsQuery.Distinct().ToList();

            MachineTypeSL = new SelectList(MachineTypeList);
        }

        public void PopulateWireMaterialSLDropDownList(WatchShopContext _context,
           object selectedDepartment = null)
        {
            var departmentsQuery = from d in _context.Watch
                                   orderby d.WireMaterial // Sort by name.
                                   select d.WireMaterial;

            var WireMaterialList = departmentsQuery.Distinct().ToList();

            WireMaterialSL = new SelectList(WireMaterialList);
        }

        public void PopulateWaterResistantSLDropDownList(WatchShopContext _context,
           object selectedDepartment = null)
        {
            var departmentsQuery = from d in _context.Watch
                                   orderby d.WaterResistant // Sort by name.
                                   select d.WaterResistant;

            var WaterResistantList = departmentsQuery.Distinct().ToList();

            WaterResistantSL = new SelectList(WaterResistantList);
        }

        public void PopulateFaceSizeSLDropDownList(WatchShopContext _context,
           object selectedDepartment = null)
        {
            var departmentsQuery = from d in _context.Watch
                                   orderby d.FaceSize // Sort by name.
                                   select d.FaceSize;

            var FaceSizeList = departmentsQuery.Distinct().ToList();

            FaceSizeSL = new SelectList(FaceSizeList);
        }
    }
}