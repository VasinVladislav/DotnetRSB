using BankApi.Models;
using Microsoft.EntityFrameworkCore;

public class BankDbContext : DbContext
{
    public DbSet<LevelOneMenu> LevelOneMenu { get; set; }
    public DbSet<LevelTwoMenu> LevelTwoMenu { get; set; }
    public DbSet<LevelThreeMenu> LevelThreeMenu { get; set; }
    public DbSet<LocationRegion> LocationRegion { get; set; }
    public DbSet<LocationCity> LocationCity { get; set; }
    public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
    {
    }
}