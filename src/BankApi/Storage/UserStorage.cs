using System.Text.RegularExpressions;
using BankApi.Models;
using Microsoft.EntityFrameworkCore;

public class UserStorage
{
    private readonly BankDbContext context;
    public UserStorage(BankDbContext context)
    {
        this.context = context;
    }
    private string CleanPhone(string phone)
    {
        return Regex.Replace(phone, @"[^\d]", "");
    }

    public async Task<BankUser> AddUser(VerifyPhoneRequest phone)
    {
        string cleanPhone = CleanPhone(phone.Phone);
        var newUser = new BankUser
        {
            Phone = cleanPhone,
            Status = UserStatus.PhoneVerified,
            CreatedAt = DateTime.UtcNow
        };
        await context.BankUsers.AddAsync(newUser);
        await context.SaveChangesAsync();
        return newUser;
    }
    public async Task<BankUser?> GetUserById(int id)
    {
        var bankUser = await context.BankUsers.FindAsync(id);
        return bankUser;
    }
    public async Task<BankUser?> GetUserByPhone(IPhoneContainer phone)
    {
        string cleanPhone = CleanPhone(phone.Phone);
        return await context.BankUsers.FirstOrDefaultAsync(u => u.Phone == cleanPhone);
    }
    public async Task<(bool isSuccess, string? token)> UpdateUser(RegisterUserRequest registerUser)
    {
        BankUser? bankUser = await GetUserByPhone(registerUser);
        if (bankUser == null) return (false, null);
        bankUser.Phone = CleanPhone(registerUser.Phone);
        bankUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerUser.Password);
        bankUser.LastName = registerUser.LastName;
        bankUser.FirstName = registerUser.FirstName;
        bankUser.MiddleName = registerUser.HasNoMiddleName ? null : registerUser.MiddleName;
        bankUser.HasNoMiddleName = registerUser.HasNoMiddleName;
        bankUser.Email = registerUser.Email;
        bankUser.BirthDate = DateTime.SpecifyKind(registerUser.BirthDate, DateTimeKind.Utc);

        string sessionToken = Guid.NewGuid().ToString();
        bankUser.SessionToken = sessionToken;

        bankUser.Status = UserStatus.Active;
        await context.SaveChangesAsync();
        return (true, sessionToken);
    }
    public async Task<BankUser?> GetUserBySessionToken(string token)
    {
        // Ищем в Supabase пользователя, у которого совпадает токен активной сессии
        return await context.BankUsers.FirstOrDefaultAsync(u => u.SessionToken == token);
    }
    public async Task<bool> ChangeStatus(int id, UserStatus status)
    {
        BankUser? bankUser = await GetUserById(id);
        if (bankUser == null) return false;
        bankUser.Status = status;
        await context.SaveChangesAsync();
        return true;
    }
    public async Task<string> UpdateSessionToken(BankUser bankUser)
    {
        string sessionToken = Guid.NewGuid().ToString();
        bankUser.SessionToken = sessionToken;
        await context.SaveChangesAsync();
        return sessionToken;
    }
    public async Task<bool> ChangePass(BankUser bankUser, ChangePasswordRequest passRequest)
    {

        // Сверяем введенный старый пароль с хэшем BCrypt в базе данных Supabase
        if (!BCrypt.Net.BCrypt.Verify(passRequest.OldPassword, bankUser.PasswordHash))
        {
            return false;
        }

        // Если старый пароль верный, хэшируем НОВЫЙ сложный пароль
        bankUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passRequest.NewPassword);

        // Просим хранилище сохранить изменения в базу данных Supabase
        await context.SaveChangesAsync();
        return true;
    }
    public async Task ChangeEmail(BankUser bankUser, ChangeEmailRequest emailRequest)
    {
        bankUser.Email = emailRequest.NewEmail;
        await context.SaveChangesAsync();
    }

}