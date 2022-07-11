
using BLL.Services;
using LeadSub.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LeadSub.Controllers
{
    public class HomeController : Controller
    {
        private SubPagesService subPagesService;

        public ActionResult ChangeLanguage(string lang)
        {
            //new LangManager().SetLanguage(lang);
            return RedirectToAction("Index", "Home");
        }

        public HomeController(SubPagesService service)
        {
            subPagesService = service;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}