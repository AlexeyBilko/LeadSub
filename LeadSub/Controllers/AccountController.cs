using DAL.Context;
using Google.Apis.Auth;
using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using LeadSub.Models;
using LeadSub.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeadSub.Controllers
{
    public class AccountController : Controller
    {
        UserManager<User> userManager;
        SignInManager<User> signInManager;
        RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<User> user, SignInManager<User> signIn, RoleManager<IdentityRole> roleManager)
        {
            userManager = user;
            signInManager = signIn;
            this.roleManager = roleManager;
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return RedirectToAction("Login", new { returnUrl = ReturnUrl });
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }
        public IActionResult AccountInfo()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult ConfirmEmail()
        {
            return View();
        }

        public IActionResult ForgotPassword()
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
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else return RedirectToAction("Index", "Home");
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
                        Code=code.ToString()
                    });
                }
                else
                {
                    ModelState.AddModelError("Email","This email or userName is alredy taken!");
                }
           
            }
            else
            {
                //ModelState.AddModelError("Email", "This email or userName is alredy taken!");
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
                    var result = await userManager.CreateAsync(user);

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
            }
                
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ChangePassword(string returnUrl)
        {
            User user = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
            return View(new ChangePasswordViewModel()
            {
                UserId = user.Id,
                ReturnUrl = returnUrl
            });

        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == model.UserId);
                var res = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (res.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                }
                else
                {
                    foreach (var item in res.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }
    }
}
