using BLL.DTO;
using BLL.Services.IdentityServices;
using LeadSub.Models;
using LeadSub.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace LeadSub.Controllers
{
    public class AccountController : Controller
    {
        UserService userService;
        SignInServcie signInService;

        public AccountController(UserService userService,SignInServcie signInServcie)
        {
            this.userService = userService;
            this.signInService = signInServcie;
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return RedirectToAction("Login", new { returnUrl = ReturnUrl });
        }
        public IActionResult Login(string returnUrl)
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> ForgotPassword()
        {
            UserDTO user = await userService.GetUser(User);
            Random rand = new Random();

            int code = rand.Next(100000, 999999);
            await EmailManager.SendText(user.Email, $"{code}");
            return View("ConfirmEmail", new ConfirmEmailViewModel
            {
                Code = code.ToString(),
                Email=user.Email,
                IsRestorePassword=true
            });

        }
        [Authorize]
        public async Task<IActionResult> AccountInfo()
        {
            UserDTO user = await userService.GetUser(User);
            AccountSettingsViewModel model = new AccountSettingsViewModel
            {
                Name = user.DisplayName,
                Email = user.Email,
                TotalFollowers = 0
            };
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult ConfirmEmail()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInService.SignInWithEmailAsync(model.Email, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong password or login");
                }
            }
            return View();
        }
     
      
        [HttpPost]
        [ValidateAntiForgeryToken]   
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await userService.FindByEmailAsync(model.Email) == null)
                {
                    Random rand = new Random();
                    int code = rand.Next(100000, 999999);
                    await EmailManager.SendText(model.Email, $"{code}");
                    return View("ConfirmEmail", new ConfirmEmailViewModel
                    {
                        UserName=model.UserName,
                        Email=model.Email,
                        Password=model.Password,
                        Code=code.ToString()
                    });
                }
                else
                {
                    ModelState.AddModelError("","This email or userName is alredy taken!");
                }
           
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ConfirmCode == model.Code) 
                {
                    UserDTO user = new UserDTO
                    {
                        Email = model.Email,
                        DisplayName = model.UserName
                    };
                    var result = await userService.CreateAsync(user,model.Password);
                    if (result.Succeeded)
                    {
                        await signInService.SignInWithEmailAsync(model.Email,model.Password);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Невірний код!");
                }
            }
                
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInService.SignOut();
            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ConfirmEmailViewModel model)
        {

            return View("RestorePassword", new RestorePasswordViewModel()
            {
                Email=model.Email
            });
        }

        [HttpPost]
        public async Task<IActionResult> RestorePassword(RestorePasswordViewModel model)
        {
            if (ModelState.IsValid) 
            {
                 var res = await userService.RestorePassword(model.Email, model.NewPassword);
                 if (res.Succeeded)
                 {
                    return RedirectToAction("Index", "Home");
                 }
                 else
                 {
                    foreach (var item in res.Errors)
                    {
                            ModelState.AddModelError("", item.Description);
                    }
                 }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(AccountSettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var res = await userService.ChangePasswordAsync(model.Email, model.NewPassword, model.ConfirmationOldPassword);
                if (res.Succeeded)
                {
                    TempData["Message"] = "Password was success changed";
                }
                else
                {
                    foreach (var item in res.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View("AccountInfo",model);
        }
    }
}
