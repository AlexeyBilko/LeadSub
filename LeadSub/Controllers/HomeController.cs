using DAL.Context;
using LeadSub.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LeadSub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        LeadSubContext context;
        public HomeController(ILogger<HomeController> logger, LeadSubContext context)
        {
            _logger = logger;
            this.context = context;

        }

        public IActionResult Index()
        {
            context.SubPages.Add(new SubPage
            {
                Avatar="",
                Description="",
                GetButtonTitle="",
                SuccessButtonTitle="",
              MainImage ="",
      SuccessDescription ="",
      
       SubscriptionsCount ="",
       ViewsCount ="",
       UserLogin="",
       InstagramLink="",
       MaterialLink ="",
        Title ="",
        Header =""

    });
            context.SaveChanges();
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