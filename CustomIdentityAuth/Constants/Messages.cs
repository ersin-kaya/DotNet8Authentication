namespace CustomIdentityAuth.Constants;

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
    
    // Security token messages
    public const string InvalidToken = "The token is invalid.";
    
    // User update messages
    public const string UserUpdateFailed = "User update failed. Please try again.";
    
    // Token generation messages
    public const string TokenGenerationSuccess = "Token generated successfully.";
    public const string RefreshTokenGenerationSuccess = "Refresh token generated successfully.";
    public const string AccessTokenGenerationFailed = "Access token could not be generated.";
    
    // Token validation messages
    public const string PrincipalExtractionSuccess = "Principal extracted successfully from the expired token.";
    
    // Refresh token messages
    public const string RefreshTokenFailed = "Failed to refresh the token. Please try again.";
    public const string InvalidRefreshToken = "The refresh token is invalid or has expired.";
    public const string RefreshTokenSuccess = "Refresh token generated successfully.";
}