using BLL.DTO;
using BLL.Services;
using DAL.Context;
using LeadSub.Models;
using LeadSub.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LeadSub.Controllers
{
    public class SubPagesController : Controller
    {
        SubPagesService subPagesService;
        UserManager<User> userManager;

        public SubPagesController(UserManager<User> user, SubPagesService subPagesService)
        {
            this.subPagesService = subPagesService;
            userManager = user;
        }
        public IActionResult MySubPages()
        {
            return View();
        }

        public IActionResult CreateSubPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubPage(SubPageViewModel subpage)
        {
            if (ModelState.IsValid)
            {
                if (subpage != null)
                {
                    User user = await userManager.GetUserAsync(User);
                    SubPageDTO dto = new SubPageDTO()
                    {
                        Avatar = Base64Encoder.GetBase64String(subpage.Avatar),
                        Title = subpage.Title,
                        Header = subpage.Header,
                        InstagramLink = subpage.InstagramLink,
                        MainImage = Base64Encoder.GetBase64String(subpage.MainImage),
                        MaterialLink = subpage.MaterialLink,
                        Description = subpage.Description,
                        GetButtonTitle = subpage.GetButtonTitle,
                        SuccessButtonTitle = subpage.SuccessButtonTitle,
                        SuccessDescription = subpage.SuccessDescription,
                        CreationDate = DateTime.Now,
                        ViewsCount = "0",
                        SubscriptionsCount = "0",
                        UserId=user.Id
                    };
                    await subPagesService.AddAsync(dto);
                    return RedirectToAction("MySubPages", "SubPages");
                }
            }
            return View();
        }
    }
}
