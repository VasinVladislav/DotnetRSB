using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public class LocationMenuInitializer
{
    private readonly BankDbContext context;
    public LocationMenuInitializer(BankDbContext context)
    {
        this.context = context;
    }

    public async Task SeedLocationMenu()
    {
        await context.Database.MigrateAsync();

        if (await context.LocationRegion.AnyAsync()) return;

        var jsonFile = "regions.json";
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var filePath = Path.Combine(AppContext.BaseDirectory, "DataSeed", "LocationMenuData", jsonFile);
        if (!File.Exists(filePath)) return;
        var jsonString = await File.ReadAllTextAsync(filePath);
        var locationMenu = JsonSerializer.Deserialize<List<LocationRegion>>(jsonString, options);
        if (locationMenu != null)
        {
            await context.LocationRegion.AddRangeAsync(locationMenu);
        }
        await context.SaveChangesAsync();
    }
}