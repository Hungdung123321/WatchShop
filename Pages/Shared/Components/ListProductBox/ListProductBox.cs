using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using WatchShop.Models;

namespace WatchShop.Pages.Shared.Components.ListProductBox
{
    public class ListProductBox : ViewComponent
    {

        public List<Watch> WatchsData;

        public async Task<IViewComponentResult> InvokeAsync(string PriceRange, string Thickness)
        {

            

            return View(WatchsData);
        }

        
    }
}
