using Microsoft.EntityFrameworkCore;
using BankApi.Models;
using System.Text.Json;

public class HeaderMenuInitializer
{
    private readonly BankDbContext context;
    public HeaderMenuInitializer(BankDbContext context)
    {
        this.context = context;
    }

    public async Task SeedHeaderMenu()
    {
        // Применяем миграции, если их нет в БД
        await context.Database.MigrateAsync();
        // Если в таблице уже есть данные — прерываем выполнение
        if (await context.LevelOneMenu.AnyAsync()) return;
        // Очищаем старые данные в Supabase перед сидингом
        // context.LevelOneMenu.RemoveRange(context.LevelOneMenu);
        // await context.SaveChangesAsync();

        // Массив имён файлов
        var jsonFiles = new[] { "individuals.json", "business.json", "private.json" };
        // Настраиваем правила для парсера JSON 
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; // Игнорировать регистр букв (text и Text будут одинаковы)

        // Пробегаемся циклом по каждому файлу
        foreach (var fileName in jsonFiles)
        {
            // Вычисляем физический путь к файлу
            var filePath = Path.Combine(AppContext.BaseDirectory, "DataSeed", "HeaderMenuData", fileName);
            // Проверяем, существует ли файл
            if (!File.Exists(filePath)) continue;

            // Читаем текст и парсим объект
            var jsonString = await File.ReadAllTextAsync(filePath);
            // Превращаем строку JSON в C# объекты
            var menuTab = JsonSerializer.Deserialize<LevelOneMenu>(jsonString, options);

            // Если парсинг прошел успешно
            if (menuTab != null)
            {
                await context.LevelOneMenu.AddAsync(menuTab);
            }
        }
        // Сохраняем всё в Supabase
        await context.SaveChangesAsync();
    }
}
