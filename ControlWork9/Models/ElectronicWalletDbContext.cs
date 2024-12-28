using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ControlWork9.Models;

public class ElectronicWalletDbContext : IdentityDbContext<MyUser, IdentityRole<int>, int>
{
    public DbSet<Transaction> Transactions { get; set; }
    public ElectronicWalletDbContext(DbContextOptions<ElectronicWalletDbContext> options) : base(options) { }
}