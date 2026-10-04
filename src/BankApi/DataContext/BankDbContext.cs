using BankApi.Models;
using Microsoft.EntityFrameworkCore;

public class BankDbContext : DbContext
{
    // Хедер меню
    public DbSet<LevelOneMenu> LevelOneMenu { get; set; }
    public DbSet<LevelTwoMenu> LevelTwoMenu { get; set; }
    public DbSet<LevelThreeMenu> LevelThreeMenu { get; set; }
    // Список регионов
    public DbSet<LocationRegion> LocationRegion { get; set; }
    public DbSet<LocationCity> LocationCity { get; set; }
    // Данные пользователя
    public DbSet<BankUser> BankUsers { get; set; }
    public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
    {
    }
}