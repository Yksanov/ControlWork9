using Microsoft.AspNetCore.Identity;

namespace ControlWork9.Models;

public class MyUser : IdentityUser<int>
{
    public int AccountNumber { get; set; }
    public decimal Balance { get; set; }
}