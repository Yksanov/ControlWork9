using ControlWork9.Models;
using ControlWork9.Repositories;
using ControlWork9.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ControlWork9.Controllers;

public class AccountController : Controller
{
    private readonly IUserManagerRepository _userManager;
    private readonly ISignInManagerRepository _signInManager;

    public AccountController(IUserManagerRepository userManager, ISignInManagerRepository signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    [HttpGet]
    public IActionResult Login(string returnUrl = "")
    {
        return View(new LoginViewModel {ReturnUrl = returnUrl});
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            MyUser user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(model.UserNameOrEmail);
            }

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Пользователь не найден");
                return View(model);
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Неправильные логин или пароль");
        }

        return View(model);
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            string accountNumber;
            do
            {
                Random random = new Random();
                accountNumber = random.Next(100000, 999999).ToString();
            }
            while ((await _userManager.GetUserByIdAsync(accountNumber)) != null);
            
            MyUser user = new MyUser
            {
                UserName = model.Username.ToLower(),
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                AccountNumber = int.Parse(accountNumber),
                Balance = 100000 
            };

            IdentityResult? result = await _userManager.CreateUserAsync(user, model.Password);

            if (result != null && result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                await _userManager.AddToRoleAsync(user, "user");
                return RedirectToAction("Index", "Home", new { accountNumber = accountNumber });
            }
            if (result?.Errors != null)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }

        return View(model);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("", "");
    }
}