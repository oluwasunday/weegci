using Microsoft.AspNetCore.Mvc;

namespace weegci.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
