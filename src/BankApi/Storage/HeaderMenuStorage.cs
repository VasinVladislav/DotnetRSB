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
    public async Task<LevelTwoMenu?> AddMenuTwo(int id, LevelTwoMenu newMenu)
    {
        // Ищем родительский заголовок вместе с его коллекцией
        var levelOneMenu = await context.LevelOneMenu
        .Include(t => t.Items)
        .FirstOrDefaultAsync(t => t.Id == id);

        if (levelOneMenu == null)
        {
            return null;
        }

        levelOneMenu.Items.Add(newMenu);
        await context.SaveChangesAsync();
        return newMenu;
    }
    public async Task<LevelThreeMenu?> AddMenuThree(int id, LevelThreeMenu newMenu)
    {
        // Ищем напрямую категорию 2-го уровня по её уникальному Id
        var levelTwoMenu = await context.LevelTwoMenu
            .Include(t => t.Items)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (levelTwoMenu == null)
        {
            return null;
        }

        levelTwoMenu.Items.Add(newMenu);
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