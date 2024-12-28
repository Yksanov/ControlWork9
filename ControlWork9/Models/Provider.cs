namespace ControlWork9.Models;

public class Provider
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public string ServiceType { get; set; } 
    public List<ServiceProvider> ServiceProviders { get; set; }
}