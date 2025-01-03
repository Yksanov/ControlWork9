using ControlWork9.Services;

namespace ControlWork9.Models;

public class Transaction
{
    public int Id { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public decimal Amount { get; set; }
    public TransactionType  Type { get; set; }
    public int FromAccount { get; set; }
    public int ToAccount { get; set; }
    public int UserId { get; set; }
    public MyUser? User { get; set; }
}