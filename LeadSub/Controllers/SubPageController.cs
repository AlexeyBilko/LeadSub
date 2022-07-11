using Microsoft.AspNetCore.Mvc;

namespace LeadSub.Controllers
{
    public class SubPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
