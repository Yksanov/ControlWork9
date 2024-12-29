using System.ComponentModel.DataAnnotations;

namespace ControlWork9.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Почта/Номер личного счета не могут быть пустыми")]
    public string AccountNumberOrEmail { get; set; }
    [Required(ErrorMessage = "Пароль не может быть пустым")]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
    public string? ReturnUrl { get; set; }
}