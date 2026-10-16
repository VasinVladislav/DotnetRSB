using BankApi.Models;
using Microsoft.AspNetCore.Mvc;

public class UserController : BaseController
{
    private readonly UserStorage userStorage;
    public UserController(UserStorage userStorage)
    {
        this.userStorage = userStorage;
    }

    public async Task<BankUser?> GetAuthorizedUserAsync()
    {
        // 1. Достаем стандартный HTTP-заголовок Authorization из текущего запроса
        string? authHeader = Request.Headers["Authorization"];

        // 2. Проверяем, что заголовок прилетел и он начинается со слова Bearer
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            return null; // Если токена нет или формат неверный, возвращаем null
        }

        // 3. Отрезаем служебное слово "Bearer " и убираем лишние пробелы
        string token = authHeader.Substring("Bearer ".Length).Trim();

        // 4. Просим наш слой данных найти и вернуть пользователя из Supabase по этому токену
        return await userStorage.GetUserBySessionToken(token);
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
        if (bankUser.Status == UserStatus.PhoneVerified)
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
        string token = await userStorage.UpdateSessionToken(bankUser);
        return Ok(new
        {
            token,
            userName = bankUser.FirstName
        });
    }
    [HttpPost("register-profile")]
    public async Task<IActionResult> RegisterProfile([FromBody] RegisterUserRequest user)
    {
        if (user == null || string.IsNullOrWhiteSpace(user.Phone) || string.IsNullOrWhiteSpace(user.Password))
            return BadRequest(new { message = "Неполные данные для регистрации" });

        var (isUpdated, token) = await userStorage.UpdateUser(user);

        if (!isUpdated)
        {
            return NotFound(new { message = "Сессия регистрации для данного номера не найдена или истекла" });
        }

        return Ok(new
        {
            token,
            userName = user.FirstName
        });
    }
    [HttpGet("profile-info")]
    public async Task<IActionResult> GetProfileInfo()
    {
        var user = await GetAuthorizedUserAsync();

        if (user == null)
            return Unauthorized(new { message = "Сессия устарела или недействительна." });

        return Ok(new
        {
            lastName = user.LastName,
            firstName = user.FirstName,
            middleName = user.MiddleName,
            hasNoMiddleName = user.HasNoMiddleName,
            email = user.Email,
            birthDate = user.BirthDate?.ToString("yyyy-MM-dd")
        });

    }
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        // Проверяем валидность входящих данных
        if (request == null || string.IsNullOrWhiteSpace(request.OldPassword) || string.IsNullOrWhiteSpace(request.NewPassword))
        {
            return BadRequest(new { message = "Все поля обязательны для заполнения" });
        }

        if (request.OldPassword == request.NewPassword)
        {
            return BadRequest(new { message = "Новый пароль не должен совпадать со старым" });
        }


        // Автоматически определяем пользователя по его токену сессии из заголовка Authorization
        var user = await GetAuthorizedUserAsync();

        if (user == null)
        {
            return Unauthorized(new { message = "Сессия устарела или недействительна. Войдите заново." });
        }

        if (await userStorage.ChangePass(user, request))
        {
            return Ok(new { message = "Пароль успешно изменен!" });
        }
        return BadRequest(new { message = "Старый пароль введен неверно" });
    }
    [HttpPost("change-email")]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.NewEmail))
        {
            return BadRequest(new { message = "Адрес электронной почты обязателен" });
        }

        // 1. Автоматически определяем пользователя по его токену сессии
        var user = await GetAuthorizedUserAsync();

        if (user == null)
        {
            return Unauthorized(new { message = "Сессия устарела или недействительна. Войдите заново." });
        }

        // 2. Финтех-защита: новая почта не должна совпадать с текущей
        if (user.Email == request.NewEmail)
        {
            return BadRequest(new { message = "Новый адрес совпадает с текущим" });
        }

        // 3. Обновляем email и просим хранилище сохранить изменения в базу
        await userStorage.ChangeEmail(user, request);

        return Ok(new { message = "Адрес электронной почты успешно изменен!" });
    }

}