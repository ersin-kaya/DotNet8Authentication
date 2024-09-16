namespace CustomIdentityAuth.Constants;

public static class ValidationConstants
{
    // Password constraints
    public const int PasswordMaxLength = 32;
    public const int PasswordMinLength = 6;

    // UserName constraints
    public const int UserNameMaxLength = 20;
    public const int UserNameMinLength = 5;

    // FirstName constraints
    public const int FirstNameMaxLength = 32;
    public const int FirstNameMinLength = 2;
    
    // LastName constraints
    public const int LastNameMaxLength = 32;
    public const int LastNameMinLength = 2;
}