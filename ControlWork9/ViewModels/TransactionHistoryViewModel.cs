using ControlWork9.Models;

namespace ControlWork9.ViewModels;

public class TransactionHistoryViewModel
{
    public DateOnly? DateFrom { get; set; }
    public DateOnly? DateTo { get; set; }
    public List<Transaction> Transactions { get; set; }
}