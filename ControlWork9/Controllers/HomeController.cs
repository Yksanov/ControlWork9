using ControlWork9.Models;
using ControlWork9.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ControlWork9.Controllers;

public class HomeController : Controller
{
    private readonly UserManager<MyUser> _userManager;

    public HomeController(UserManager<MyUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        ViewData["AccountNumber"] = user?.AccountNumber;
            
        // Отображаем баланс текущего пользователя на главной странице
        ViewData["Balance"] = user.Balance;

        // Проверка на наличие успешных сообщений (например, после перевода)
        ViewBag.SuccessMessage = TempData["SuccessMessage"];
        ViewBag.ErrorMessage = TempData["ErrorMessage"];

        var model = new DepositViewModel();

        return View(model);
    }
}