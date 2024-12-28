using ControlWork9.Models;
using Microsoft.AspNetCore.Identity;

namespace ControlWork9.Repositories;

public class SignInManagerRepository : ISignInManagerRepository
{
    private readonly SignInManager<MyUser> _signInManager;

    public SignInManagerRepository(SignInManager<MyUser> signInManager)
    {
        _signInManager = signInManager;
    }
    public async Task SignOutAsync()
    { 
        await _signInManager.SignOutAsync();
    }

    public async Task<SignInResult> PasswordSignInAsync(MyUser user, string password, bool isPersistent, bool lockoutOnFailure)
    {
        return await _signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
    }

    public async Task SignInAsync(MyUser user, bool isPersistent)
    {
        await _signInManager.SignInAsync(user, isPersistent);
    }
}