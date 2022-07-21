using BLL.DTO;
using BLL.Services;
using BLL.Services.IdentityServices;
using LeadSub.Models;
using LeadSub.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LeadSub.Controllers
{
    public class SubPagesController : Controller
    {
        SubPagesService subPagesService;
        UserService userManager;

        public SubPagesController(UserService user, SubPagesService subPagesService)
        {
            this.subPagesService = subPagesService;
            userManager = user;
        }
        public async Task<IActionResult> MySubPages()
        {
            UserDTO user = await userManager.GetUser(User);
            IEnumerable<SubPageDTO> res = await subPagesService.GetMyAsync(user.Id);
            return View(res);
        }

        public IActionResult CreateSubPage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSubPage(int Id)
        {
            SubPageDTO subPage= await subPagesService.DeleteAsync(Id);
            if (subPage != null) 
            {
                TempData["Message"] = "SubPage is deleted!";
            }
            return RedirectToAction("MySubPages", "SubPages");
        }

            [HttpPost]
        public async Task<IActionResult> CreateSubPage(SubPageViewModel subpage)
        {
            if (ModelState.IsValid)
            {
                if (subpage != null)
                {
                    string userId = userManager.GetUserId(User);
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
                        ViewsCount = 0,
                        SubscriptionsCount = 0,
                        UserId=userId
                    };
                    await subPagesService.AddAsync(dto);
                    TempData["Message"] = "New subscription page is created!";
                    return RedirectToAction("MySubPages", "SubPages");
                }
            }
            return View();
        }
    }
}
