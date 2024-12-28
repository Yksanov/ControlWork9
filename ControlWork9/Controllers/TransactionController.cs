using ControlWork9.Models;
using ControlWork9.Services;
using ControlWork9.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlWork9.Controllers;

public class TransactionController : Controller
{
    private readonly ElectronicWalletDbContext _context;
    private readonly UserManager<MyUser> _userManager;

    public TransactionController(ElectronicWalletDbContext context, UserManager<MyUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }


    [HttpPost]
    public async Task<IActionResult> Deposit(DepositViewModel model)
    {
        if (ModelState.IsValid)
        {
            MyUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.AccountNumber == model.ToAccount);

            if (user == null)
            {
                return NotFound();
            }

            Transaction transaction = new Transaction()
            {
                Amount = model.Amount,
                Type = TransactionType.Deposit,
                UserId = user.Id,
                Date = DateOnly.FromDateTime(DateTime.Now),
                FromAccount = user.AccountNumber,
                ToAccount = model.ToAccount
            };

            _context.Transactions.Add(transaction);

            user.Balance += model.Amount;

            _context.Update(user);
            await _context.SaveChangesAsync();

            ViewData["Balance"] = user.Balance;
            return PartialView("_DepositFormPartialView", model);
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid deposit");
            return PartialView("_DepositFormPartialView", model);
        }
    }
    //-------------------------------------------------


    [HttpPost]
    public async Task<IActionResult> Transfer(TransferViewModel model)
    {
        if (ModelState.IsValid)
        {
            MyUser? sender = await _userManager.GetUserAsync(User);
            if (sender == null)
            {
                return Unauthorized();
            }
            MyUser? recipient = await _userManager.Users.FirstOrDefaultAsync(u => u.AccountNumber == model.ToAccount);
            if (recipient == null)
            {
                ModelState.AddModelError(string.Empty, "Получатель с указанным номером счета не найден.");
                return PartialView("_TransferFormPartialView", model);
            }
            if (sender.Balance < model.Amount)
            {
                ModelState.AddModelError(string.Empty, "Недостаточно средств для перевода.");
                return PartialView("_TransferFormPartialView", model);
            }
            Transaction transaction = new Transaction()
            {
                Amount = model.Amount,
                Type = TransactionType.Transfer,
                FromAccount = sender.AccountNumber,
                ToAccount = recipient.AccountNumber,
                Date = DateOnly.FromDateTime(DateTime.Now),
                UserId = sender.Id
            };

            sender.Balance -= model.Amount;
            recipient.Balance += model.Amount;

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            ViewData["Balance"] = sender.Balance;

            return PartialView("_TransferFormPartialView", new TransferViewModel());
        }

        return PartialView("_TransferFormPartialView", model);
    }
    
    //-----------------------------------------------------------
    
    public async Task<IActionResult> TransactionHistory(DateOnly? dateFrom, DateOnly? dateTo)
    {
        MyUser? user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return Unauthorized();
        }
        var transactionsQuery = _context.Transactions
            .Where(t => t.UserId == user.Id);

        if (dateFrom.HasValue)
        {
            transactionsQuery = transactionsQuery.Where(t => t.Date >= dateFrom.Value);
        }

        if (dateTo.HasValue)
        {
            transactionsQuery = transactionsQuery.Where(t => t.Date <= dateTo.Value);
        }

        var transactions = await transactionsQuery
            .OrderByDescending(t => t.Date) 
            .ToListAsync();
        var model = new TransactionHistoryViewModel
        {
            DateFrom = dateFrom,
            DateTo = dateTo,
            Transactions = transactions
        };

        return View(model);
    }


}