namespace ControlWork9.Models;

public class UserProvider
{
    public int Id { get; set; } 
    public int UserId { get; set; }
    public int ServiceProviderId { get; set; }

    public int PersonalAccount { get; set; }
    public decimal Balance { get; set; }

    public ServiceProvider? ServiceProvider { get; set; }
    public MyUser? User { get; set; }
}