using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeadSub.Controllers
{
    public class SubPageController : Controller
    {
        SubPagesService subPagesService;
        SubPageDTO currentSubPage;
        public SubPageController(SubPagesService service)
        {
            subPagesService = service;
        }
        public async Task<IActionResult> Index(int id)
        {
            currentSubPage=await subPagesService.GetAsync(id);
            return View("ThirdPage",currentSubPage);
        }
        public async Task<IActionResult> ThirdPage()
        {
            return View(currentSubPage);
        }
    }
}
