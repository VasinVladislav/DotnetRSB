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
    public string FirstName { get; set; } = string.Empty;
}
