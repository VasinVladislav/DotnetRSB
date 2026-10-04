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
    public async Task<bool> UpdateUser(RegisterUserRequest registerUser)
    {
        BankUser? bankUser = await GetUserByPhone(registerUser);
        if (bankUser == null) return false;
        bankUser.Phone = CleanPhone(registerUser.Phone);
        bankUser.PasswordHash = registerUser.Password;
        bankUser.FirstName = registerUser.FirstName;
        bankUser.Status = UserStatus.Active;
        await context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> ChangeStatus(int id, UserStatus status)
    {
        BankUser? bankUser = await GetUserById(id);
        if (bankUser == null) return false;
        bankUser.Status = status;
        await context.SaveChangesAsync();
        return true;
    }
}