using Microsoft.AspNetCore.Mvc;

namespace weegci.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
