using System.ComponentModel.DataAnnotations;

namespace ControlWork9.ViewModels;

public class TransferViewModel
{
    [Required(ErrorMessage = "Введите номер счета получателя.")]
    public int ToAccount { get; set; }

    [Required(ErrorMessage = "Введите сумму перевода.")]
    [Range(1, double.MaxValue, ErrorMessage = "Сумма перевода должна быть больше 0.")]
    public decimal Amount { get; set; }
}