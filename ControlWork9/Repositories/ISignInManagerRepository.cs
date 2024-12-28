using ControlWork9.Models;
using Microsoft.AspNetCore.Identity;

namespace ControlWork9.Repositories;

public interface ISignInManagerRepository
{
    public Task SignOutAsync();
    public Task<SignInResult> PasswordSignInAsync(MyUser user, string password, bool isPersistent, bool lockoutOnFailure);
    public Task SignInAsync(MyUser user, bool isPersistent);
}