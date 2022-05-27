
using BLL.Services;
using LeadSub.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LeadSub.Controllers
{
    public class HomeController : Controller
    {
        //LeadSubContext context;
        private SubPagesService subPagesService;
        private AspNetIdentityContext AspNetIdentityContext;
        public HomeController(SubPagesService service, AspNetIdentityContext aspNetIdentityContext)
        {
            subPagesService = service;
            aspNetIdentityContext.Users.Add(new Models.User());
            aspNetIdentityContext.SaveChanges();
        }

        public IActionResult Index()
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