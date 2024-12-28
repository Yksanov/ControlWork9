namespace ControlWork9.Models;

public class ServiceProvider
{
    public int Id { get; set; } 
    public int ProviderId { get; set; }
    public Provider? Provider { get; set; } 

    public string ServiceName { get; set; } 
    public decimal Price { get; set; }
    public List<UserProvider> UserProviders { get; set; }
}