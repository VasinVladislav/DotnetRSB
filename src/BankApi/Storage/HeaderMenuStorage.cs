using BankApi.Models;
using Microsoft.EntityFrameworkCore;
public class HeaderMenuStorage
{
    private readonly BankDbContext context;

    public HeaderMenuStorage(BankDbContext context)
    {
        this.context = context;
    }
    public async Task<LevelOneMenu> AddMenu(LevelOneMenu newMenu)
    {

        await context.LevelOneMenu.AddAsync(newMenu);
        await context.SaveChangesAsync();
        return newMenu;
    }
    public async Task<List<LevelOneMenu>> GetMenu()
    {
        return await context.LevelOneMenu
            .OrderBy(l1 => l1.SortOrder) // Сортируем табы
            .Include(l1 => l1.Items.OrderBy(l2 => l2.SortOrder)) // Сортируем категории
            .ThenInclude(l2 => l2.Items.OrderBy(l3 => l3.SortOrder)) // Сортируем ссылки
            .ToListAsync();
    }
}