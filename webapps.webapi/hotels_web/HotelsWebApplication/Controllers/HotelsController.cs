namespace HotelsWebApplication.Controllers
{
    using System.Collections.Generic;
    using HotelsApiClient;
    using Microsoft.AspNetCore.Mvc;

    public class HotelsController : Controller
    {
        public IActionResult Index()
        {
            return this.View(new List<HotelResource>());
        }
    }
}