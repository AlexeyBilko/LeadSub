using Microsoft.AspNetCore.Mvc;

namespace LeadSub.Controllers
{
    public class SubPagesController : Controller
    {
        public IActionResult MySubPages()
        {
            return View();
        }
    }
}
