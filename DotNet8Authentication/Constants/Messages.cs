namespace DotNet8Authentication.Constants;

public static class Messages
{
    // General operation messages
    public const string OperationSuccess = "Operation completed successfully.";
    public const string OperationFailed = "Operation failed.";
    
    // Registration messages
    public const string RegistrationSuccess = "Registration completed successfully.";
    public const string RegistrationError = "An error occurred during registration.";
    public const string EmailAlreadyInUse = "The email address is already in use.";

    // Login messages
    public const string LoginSuccess = "Login successful.";
    public const string LoginError = "An error occurred during login.";
    public const string InvalidLoginAttempt = "Invalid email or password.";
    
    // Role assignment messages
    public const string RoleAssignmentError = "An error occurred while assigning the role.";
}