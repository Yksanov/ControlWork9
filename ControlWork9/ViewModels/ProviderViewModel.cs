using ControlWork9.Models;

namespace ControlWork9.ViewModels;

public class ProviderViewModel
{
    public int UserId { get; set; }
    public decimal UserBalance { get; set; }
    public List<ControlWork9.Models.ServiceProvider> ServiceProviders { get; set; }
    public List<UserProvider> UserServices { get; set; }
}