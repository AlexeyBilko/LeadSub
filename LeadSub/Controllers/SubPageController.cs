using BLL.DTO;
using BLL.Services;
using LeadSub.Models;
using LeadSub.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LeadSub.Controllers
{
    public class SubPageController : Controller
    {
        SubPagesService subPagesService;

        public SubPageController(SubPagesService service)
        {
            subPagesService = service;
        }
        public async Task<IActionResult> FirstPage(int id)
        {
            SubPageDTO currentSubPage = await subPagesService.GetAsync(id);
            return View(currentSubPage);
        }

        public async Task<IActionResult> Initial(int id)
        {
            SubPageDTO currentSubPage = await subPagesService.GetAsync(id);
            return View("Initial", new SubscriptionCheckerViewModel()
            {
                Username = "",
                subPageDTO = currentSubPage
            });
        }

        [HttpPost]
        public async Task<IActionResult> InitialGetUsername(SubscriptionCheckerViewModel model)
        {
            return View("Check", new SubscriptionCheckerViewModel
            {
                Username = model.Username,
                subPageDTO = model.subPageDTO
            });
        }

        public IActionResult Success()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> CkeckSubscription(SubscriptionCheckerViewModel model)
        {
            SubscriptionChecker sc = new SubscriptionChecker();
            return await sc.GetFollowers(subscribeTo: model.subPageDTO.InstagramLink, username: model.Username) ? View("Success", model) : (IActionResult)View("Check", model);
        }

    }
}
