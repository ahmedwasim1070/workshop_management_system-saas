namespace Backend.DTOs.Auth;

public record SigninRequestDto(
    string FullName,
    string Email,
    string Password,
    string ConfirmPassword
);