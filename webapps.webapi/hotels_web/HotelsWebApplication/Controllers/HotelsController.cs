namespace HotelsWebApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HotelsController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}