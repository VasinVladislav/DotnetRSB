using Microsoft.EntityFrameworkCore;

public class LocationMenuStorage
{
    private readonly BankDbContext context;
    public LocationMenuStorage(BankDbContext context)
    {
        this.context = context;
    }
    public async Task<LocationRegion> AddRegion(LocationRegion locationRegion)
    {
        await context.LocationRegion.AddAsync(locationRegion);
        await context.SaveChangesAsync();
        return locationRegion;
    }
    public async Task<LocationCity?> AddCity(int id, LocationCity locationCity)
    {
        // Ищем родительский заголовок вместе с его коллекцией
        var region = await context.LocationRegion
        .Include(t => t.Cities)
        .FirstOrDefaultAsync(t => t.Id == id);

        if (region == null)
        {
            return null;
        }

        region.Cities.Add(locationCity);
        await context.SaveChangesAsync();
        return locationCity;
    }
    public async Task<List<LocationRegion>> GetLocation()
    {
        return await context.LocationRegion
        // Сортируем регионы по алфавиту (от А до Я) на основе поля Text
        .OrderBy(r => r.Text) 
        .Include(r => r.Cities.OrderBy(c => c.Text)) 
        .ToListAsync();
    }
}