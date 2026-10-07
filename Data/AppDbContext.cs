using Villaalba_Midterm_Store.Models;
using Microsoft.EntityFrameworkCore;

namespace Villaalba_Midterm_Store.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
}