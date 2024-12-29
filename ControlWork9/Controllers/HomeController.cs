using ControlWork9.Models;
using ControlWork9.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        

        ViewData["AccountNumber"] = user?.AccountNumber;
        ViewData["Balance"] = user?.Balance;
        ViewBag.SuccessMessage = TempData["SuccessMessage"];
        ViewBag.ErrorMessage = TempData["ErrorMessage"];

        DepositViewModel model = new DepositViewModel();

        return View(model);
    }
}