using BankApi.Models;
using Microsoft.AspNetCore.Mvc;

public class HeaderMenuController : BaseController
{
    private HeaderMenuStorage _menuStorage;
    public HeaderMenuController(HeaderMenuStorage menuStorage)
    {
        _menuStorage = menuStorage;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LevelOneMenu menu)
    {
        try
        {
            LevelOneMenu createdMenu = await _menuStorage.AddMenu(menu);

            return CreatedAtAction(nameof(Get), new { id = createdMenu.Id }, createdMenu);
        }
        catch (Exception ex)
        {
            return Conflict(new { message = "Ошибка добавления меню", details = ex.Message });
        }
    }
    [HttpGet]
    public async Task<ActionResult<List<LevelOneMenu>>> Get()
    {
        var menu = await _menuStorage.GetMenu();
        return Ok(menu);
    }
}