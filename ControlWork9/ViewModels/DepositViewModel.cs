using System.ComponentModel.DataAnnotations;

namespace ControlWork9.ViewModels;

public class DepositViewModel
{
    [Required]
    [Range(100000, 999999, ErrorMessage = "Номер счета должен быть 6 знаков.")]
    public int ToAccount { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Сумма перевода должна быть больше 0.")]
    public decimal Amount { get; set; }
}