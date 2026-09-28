using BankApi.Models;
using Microsoft.AspNetCore.Mvc;

public class HeaderMenuController : BaseController
{
    private HeaderMenuStorage menuStorage;
    public HeaderMenuController(HeaderMenuStorage menuStorage)
    {
        this.menuStorage = menuStorage;
    }

    [HttpGet]
    public async Task<ActionResult<List<LevelOneMenu>>> GetHeader()
    {
        var menu = await menuStorage.GetMenu();
        return Ok(menu);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLevelOne([FromBody] LevelOneMenu menu)
    {
        try
        {
            LevelOneMenu createdMenu = await menuStorage.AddMenu(menu);

            return CreatedAtAction(nameof(GetHeader), new { id = createdMenu.Id }, createdMenu);
        }
        catch (Exception ex)
        {
            return Conflict(new { message = "Ошибка добавления меню", details = ex.Message });
        }
    }
    [HttpPost("{id:int}/category")]
    public async Task<IActionResult> CreateLevelTwo(int id, [FromBody] LevelTwoMenu menu)
    {
        try
        {
            var result = await menuStorage.AddMenuTwo(id, menu);

            if (result == null)
            {
                return NotFound(new { message = $"Не удалось добавить ссылку. Категория 1-го уровня с Id = {id} не существует." });
            }

            // Ссылаемся на метод получения всего меню, чтобы сбросить кэш на фронтенде
            return CreatedAtAction(nameof(GetHeader), null, result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ошибка при добавлении элемента 2-го уровня", details = ex.Message });
        }
    }
    [HttpPost("{id:int}/subcategory")]
    public async Task<IActionResult> CreateLevelThree(int id, [FromBody] LevelThreeMenu menu)
    {
        try
        {
            var result = await menuStorage.AddMenuThree(id, menu);

            if (result == null)
            {
                return NotFound(new { message = $"Не удалось добавить ссылку. Категория 2-го уровня с Id = {id} не существует." });
            }

            // Ссылаемся на метод получения всего меню, чтобы сбросить кэш на фронтенде
            return CreatedAtAction(nameof(GetHeader), null, result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ошибка при добавлении элемента 3-го уровня", details = ex.Message });
        }
    }
}