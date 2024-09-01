namespace CustomIdentityAuth.Constants;

public static class ValidationConstants
{
    // Password constraints
    public const int PasswordMaxLength = 32;
    public const int PasswordMinLength = 6;

    // UserName constraints
    public const int UserNameMaxLength = 20;
    public const int UserNameMinLength = 5;

    // FullName constraints
    public const int FullNameMaxLength = 64;
    public const int FullNameMinLength = 4;
}