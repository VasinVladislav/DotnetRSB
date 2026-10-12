public class VerifyPhoneRequest : IPhoneContainer
{
    public string Phone { get; set; } = string.Empty;
}

public class LoginRequest : IPhoneContainer
{
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class RegisterUserRequest : IPhoneContainer
{
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; } // Опционально
    public bool HasNoMiddleName { get; set; }
    public string Email { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}
