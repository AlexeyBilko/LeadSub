using BLL.DTO;
using BLL.Services;
using BLL.Services.IdentityServices;
using LeadSub.Models;
using LeadSub.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeadSub.Controllers
{
    [Authorize]
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
            IEnumerable<SubPageDTO> res = await subPagesService.GetAllAsync();
            return View(res);
        }
        public async Task<IActionResult> CreateSubPage(int? id)
        {
            if (id != null)
            {
                SubPageDTO page = await subPagesService.GetAsync((int)id);
                if (page != null) 
                {
                    SubPageViewModel model = new SubPageViewModel();

                    model.SubPageId = page.Id;
                    model.Description = page.Description;
                    model.Header = page.Header;
                    model.SuccessButtonTitle = page.SuccessButtonTitle;
                    model.GetButtonTitle = page.GetButtonTitle;
                    model.Title= page.Title;
                    model.InstagramLink = page.InstagramLink;
                    model.MaterialLink=page.MaterialLink;
                    model.SuccessDescription = page.SuccessDescription;

                    model.AvatarBase64 = page.Avatar;
                    model.MainImageBase64 = page.MainImage;

                    model.SubscriptionsCount = page.SubscriptionsCount;
                    model.ViewsCount = page.ViewsCount;
                    model.CreationDate = page.CreationDate;
                    return View(model);
                }
            }
            return View(new SubPageViewModel());
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
                        Avatar = subpage.Avatar!=null?Base64Encoder.GetBase64String(subpage.Avatar):subpage.AvatarBase64,
                        Title = subpage.Title,
                        Header = subpage.Header,
                        InstagramLink = subpage.InstagramLink,
                        MainImage = subpage.MainImage != null ? Base64Encoder.GetBase64String(subpage.MainImage) : subpage.MainImageBase64,
                        MaterialLink = subpage.MaterialLink,
                        Description = subpage.Description,
                        GetButtonTitle = subpage.GetButtonTitle,
                        SuccessButtonTitle = subpage.SuccessButtonTitle,
                        SuccessDescription = subpage.SuccessDescription,
                        CreationDate =subpage.CreationDate,
                        ViewsCount = subpage.ViewsCount,
                        SubscriptionsCount = subpage.SubscriptionsCount,
                        UserId=userId
                    };
                    if (subpage.SubPageId != 0)
                    {
                        dto.Id = subpage.SubPageId;
                        await subPagesService.UpdateAsync(dto);
                        TempData["Message"] = "Subscription page is edited!";
                    }
                    else
                    {
                        await subPagesService.AddAsync(dto);
                        TempData["Message"] = "New subscription page is created!";
                    }
                    return RedirectToAction("MySubPages", "SubPages");
                }
            }
            return View();
        }
    }
}
