using System.Security.Claims;
using ControlWork9.Models;
using Microsoft.AspNetCore.Identity;

namespace ControlWork9.Repositories;

public class UserManagerRepository : IUserManagerRepository
{
    private readonly UserManager<MyUser> _userManager;

    public UserManagerRepository(UserManager<MyUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<MyUser?> GetUserAsync(ClaimsPrincipal user)
    {
        return await _userManager.GetUserAsync(user);
    }

    public async Task<MyUser?> GetUserByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<IdentityResult?> CreateUserAsync(MyUser user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<IdentityResult?> UpdateUserAsync(MyUser user)
    {
        return await _userManager.UpdateAsync(user);
    }

    public async Task<bool> IsInRoleAsync(MyUser user, string role)
    {
        return await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<MyUser> FindByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<MyUser?> FindByNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public async Task<IdentityResult> AddToRoleAsync(MyUser user, string role)
    {
        return await _userManager.AddToRoleAsync(user, role);
    }
}