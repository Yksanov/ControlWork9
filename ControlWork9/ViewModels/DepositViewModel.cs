using System.ComponentModel.DataAnnotations;

namespace ControlWork9.ViewModels;

public class DepositViewModel
{
    [Required(ErrorMessage = "Номер счета не должен быть пустым")]
    [Range(100000, 999999, ErrorMessage = "Номер счета должен быть 6 знаков.")]
    public int ToAccount { get; set; }

    [Required(ErrorMessage = "Сумма не должен быть пустым")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Сумма перевода должна быть больше 0.")]
    public decimal Amount { get; set; }
}