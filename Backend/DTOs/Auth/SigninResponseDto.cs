namespace Backend.DTOs.Auth;

public record SigninResponseDto(
    string FullName,
    string Email
);