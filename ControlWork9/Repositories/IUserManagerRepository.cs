using System.Security.Claims;
using ControlWork9.Models;
using Microsoft.AspNetCore.Identity;

namespace ControlWork9.Repositories;

public interface IUserManagerRepository
{
    public Task<MyUser?> GetUserAsync(ClaimsPrincipal user);
    public Task<MyUser?> GetUserByIdAsync(string userId);
    public Task<IdentityResult?> CreateUserAsync(MyUser user, string password);
    public Task<IdentityResult?> UpdateUserAsync(MyUser user);
    public Task<bool> IsInRoleAsync(MyUser user, string role);
    public Task<MyUser?> FindByEmailAsync(string email);
    public Task<MyUser?> FindByNameAsync(string userName);
    public Task<IdentityResult> AddToRoleAsync(MyUser user, string role);
}