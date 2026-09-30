using Microsoft.AspNetCore.Mvc;

public class LocationMenuController : BaseController
{
    private LocationMenuStorage menuStorage;
    public LocationMenuController(LocationMenuStorage menuStorage)
    {
        this.menuStorage = menuStorage;
    }
    [HttpGet]
    public async Task<ActionResult<List<LocationRegion>>> GetLocation()
    {
        var location = await menuStorage.GetLocation();
        return Ok(location);
    }
    [HttpPost]
    public async Task<IActionResult> CreateRegion([FromBody] LocationRegion region)
    {
        try
        {
            LocationRegion createdRegion = await menuStorage.AddRegion(region);

            return CreatedAtAction(nameof(GetLocation), new { id = createdRegion.Id }, createdRegion);
        }
        catch (Exception ex)
        {
            return Conflict(new { message = "Ошибка добавления города", details = ex.Message });
        }
    }
    [HttpPost("{id:int}/regions")]
    public async Task<IActionResult> CreateCity(int id, [FromBody] LocationCity city)
    {
        try
        {
            var result = await menuStorage.AddCity(id, city);

            if (result == null)
            {
                return NotFound(new { message = $"Не удалось добавить город. Категория локации с Id = {id} не существует в базе." });
            }

            return CreatedAtAction(nameof(CreateRegion), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            // На случай непредвиденных ошибок (например, сбой сети с Supabase)
            return StatusCode(500, new { message = "Внутренняя ошибка сервера при добавлении города", details = ex.Message });
        }
    }
}