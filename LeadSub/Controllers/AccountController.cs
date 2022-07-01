using DAL.Context;
using LeadSub.Models;
using LeadSub.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeadSub.Controllers
{
    public class AccountController : Controller
    {
        UserManager<User> userManager;
        SignInManager<User> signInManager;

        public AccountController(UserManager<User> user, SignInManager<User> signIn, RoleManager<IdentityRole> roleManager)
        {
            userManager = user;
            signInManager = signIn;
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
            User user = await userManager.GetUserAsync(User);
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
            User user = await userManager.GetUserAsync(User);
            AccountSettingsViewModel model = new AccountSettingsViewModel
            {
                Name = user.UserName,
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
                User user = await userManager.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

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
                if (await userManager.FindByEmailAsync(model.Email) == null&&await userManager.FindByNameAsync(model.UserName)==null)
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
                    User user = new User
                    {
                        Email = model.Email,
                        UserName = model.UserName
                    };
                    var result = await userManager.CreateAsync(user,model.Password);

                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, false);
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
                
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
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
                User user = await userManager.FindByEmailAsync(model.Email);
               if(!await userManager.CheckPasswordAsync(user, model.NewPassword))
               {
                    string resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                    var res = await userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);
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
               else
               {
                  ModelState.AddModelError("", "Новий пароль не може співпадати зі старим!");
               }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(AccountSettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByEmailAsync(model.Email);
                var res = await userManager.ChangePasswordAsync(user, model.ConfirmationOldPassword, model.NewPassword);
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
