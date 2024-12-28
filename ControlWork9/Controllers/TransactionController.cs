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
            // Поиск пользователя по номеру счета
            MyUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.AccountNumber == model.ToAccount);

            if (user == null)
            {
                return NotFound();
            }

            // Создание транзакции
            Transaction transaction = new Transaction()
            {
                Amount = model.Amount,
                Type = TransactionType.Deposit,
                UserId = user.Id,
                Date = DateTime.Now,
                FromAccount = user.AccountNumber, 
                ToAccount = model.ToAccount
            };

            // Использование транзакции базы данных
            using var dbTransaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Transactions.Add(transaction); // Добавление транзакции в БД

                user.Balance += model.Amount; // Обновление баланса пользователя
                _context.Users.Update(user);  // Обновление пользователя

                await _context.SaveChangesAsync(); // Сохранение изменений в базе данных

                await dbTransaction.CommitAsync(); // Подтверждение транзакции

                // Передача нового баланса в ViewData
                ViewData["Balance"] = user.Balance;

                // Возвращение обновленного частичного представления
                return PartialView("_DepositFormPartialView", model);
            }
            catch
            {
                await dbTransaction.RollbackAsync(); // Откат изменений в случае ошибки
                ModelState.AddModelError(string.Empty, "Ошибка при пополнении баланса");
            }
        }

        // Если модель недействительна или возникла ошибка
        ModelState.AddModelError(string.Empty, "Неверные данные для пополнения");
        return PartialView("_DepositFormPartialView", model);
    }

    
}