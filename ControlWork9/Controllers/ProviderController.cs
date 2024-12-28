using ControlWork9.Models;
using ControlWork9.Services;
using ControlWork9.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlWork9.Controllers;

public class ProviderController : Controller
{
    private readonly ElectronicWalletDbContext _context;
    private readonly UserManager<MyUser> _userManager;

    public ProviderController(ElectronicWalletDbContext context, UserManager<MyUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    public async Task<IActionResult> Index()
    {
        if (!int.TryParse(_userManager.GetUserId(User), out int userId))
        {
            return BadRequest();
        }

        var user = await _context.Users.FindAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        var serviceProviders = await _context.ServiceProviders.ToListAsync();
        var userServices = await _context.UserServices
            .Where(us => us.UserId == userId)
            .ToListAsync();

        var viewModel = new ProviderViewModel
        {
            UserId = userId,
            UserBalance = user.Balance,
            ServiceProviders = serviceProviders,
            UserServices = userServices
        };

        return View(viewModel);
    }


    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Subscribe(int userId, int serviceProviderId, int personalAccount)
    {
        var serviceProvider = await _context.ServiceProviders.FirstOrDefaultAsync(sp => sp.Id == serviceProviderId);

        if (serviceProvider == null)
        {
            return NotFound();
        }

        var existingSubscription = await _context.UserServices
            .FirstOrDefaultAsync(us => us.UserId == userId && us.ServiceProviderId == serviceProviderId);

        if (existingSubscription != null)
        {
            return BadRequest();
        }

        var userProvider = new UserProvider
        {
            UserId = userId,
            ServiceProviderId = serviceProviderId,
            PersonalAccount = personalAccount,
            Balance = 0 
        };

        _context.UserServices.Add(userProvider);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Pay(int userId, int serviceProviderId, decimal amount)
    {
        var userProvider = await _context.UserServices.FirstOrDefaultAsync(up => up.UserId == userId && up.ServiceProviderId == serviceProviderId);

        if (userProvider == null)
        {
            return NotFound();
        }

        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return NotFound();
        }
        
        if (user.Balance < amount)
        {
            return BadRequest();
        }
        
        user.Balance -= amount;
        userProvider.Balance += amount;
        
        _context.Update(user);
        _context.Update(userProvider);
        await _context.SaveChangesAsync();

        var transaction = new Transaction
        {
            UserId = userId,
            FromAccount = user.AccountNumber,
            ToAccount = userProvider.PersonalAccount,
            Amount = amount,
            Type = TransactionType.Payment,
            Date = DateOnly.FromDateTime(DateTime.Now)
        };
        
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}