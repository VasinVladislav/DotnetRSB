using BankApi.Models;
using Microsoft.AspNetCore.Mvc;

public class UserController : BaseController
{
    private readonly UserStorage userStorage;
    public UserController(UserStorage userStorage)
    {
        this.userStorage = userStorage;
    }

    [HttpPost("verify-phone")]
    public async Task<ActionResult<BankUser>> VerifyUser([FromBody] VerifyPhoneRequest phone)
    {
        if (phone == null || string.IsNullOrWhiteSpace(phone.Phone))
            return BadRequest(new { message = "Номер телефона обязателен для заполнения" });

        var bankUser = await userStorage.GetUserByPhone(phone);
        // Сценарий А: Номер новый. Фиксируем его в базе и отправляем на регистрацию.
        if (bankUser == null)
        {
            await userStorage.AddUser(phone);
            return Ok(new { action = "register_profile", message = "Номер подтвержден. Заполните профиль." });
        }
        // Сценарий Б: Пользователь бросил заполнение профиля в прошлый раз
        if (bankUser.Status == UserStatus.PendingDetails)
        {
            return Ok(new { action = "register_profile", message = "Продолжите регистрацию профиля." });
        }
        // Сценарий В: Пользователь заблокирован службой безопасности
        if (bankUser.Status == UserStatus.Blocked)
        {
            return StatusCode(403, new { message = "Доступ заблокирован службой безопасности банка." });
        }
        // Сценарий Г: Клиент активен, отправляем на ввод пароля для входа
        return Ok(new { action = "request_password", message = "Пользователь найден." });
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest login)
    {
        if (login == null || string.IsNullOrWhiteSpace(login.Phone) || string.IsNullOrWhiteSpace(login.Password))
            return BadRequest(new { message = "Все поля обязательны для заполнения" });

        var bankUser = await userStorage.GetUserByPhone(login);

        if (bankUser == null || bankUser.Status != UserStatus.Active)
        {
            return BadRequest(new { message = "Некорректный запрос авторизации" });
        }
        if (!BCrypt.Net.BCrypt.Verify(login.Password, bankUser.PasswordHash))
        {
            return BadRequest(new { message = "Неверный пароль" });
        }
        return Ok(new
        {
            token = "mock-jwt-token-active-user",
            userName = bankUser.FirstName
        });
    }
    [HttpPost("register-profile")]
    public async Task<IActionResult> RegisterProfile([FromBody] RegisterUserRequest user)
    {
        if (user == null || string.IsNullOrWhiteSpace(user.Phone) || string.IsNullOrWhiteSpace(user.Password))
            return BadRequest(new { message = "Неполные данные для регистрации" });

        bool isUpdated = await userStorage.UpdateUser(user);

        if (!isUpdated)
        {
            return NotFound(new { message = "Сессия регистрации для данного номера не найдена или истекла" });
        }

        return Ok(new
        {
            token = "mock-jwt-token-newly-registered",
            userName = user.FirstName
        });
    }
}