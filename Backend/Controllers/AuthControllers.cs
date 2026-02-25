using Backend.Common;
using Backend.DTOs.Auth;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthControllers : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly TokenServices _tokenService;


    public AuthControllers(AppDbContext context, TokenServices tokenServices)
    {
        _context = context;
        _tokenService = tokenServices;
    }

    [HttpPost("signin")]
    public async Task<ActionResult<ApiResponse<Object>>> Signin(SigninRequestDto request)
    {
        try
        {
            var user = await _context.Users.AnyAsync(u => u.Email == request.Email);
            if (user)
                return Conflict(ApiResponse<Object>.Fail("Email already in use."));

            if (request.Password != request.ConfirmPassword)
                return BadRequest(ApiResponse<Object>.Fail("Wrong Password and Confirm Password."));

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var newUser = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                Password = hashedPassword
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<Object>.Ok(null, "Successfully Created User."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<Object>.Fail(ex.Message));
        }
    }

    [HttpGet("login")]
    public async Task<ActionResult<ApiResponse<Object>>> Login(LoginRequestDto request)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return NotFound(ApiResponse<Object>.Fail("User not found."));

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return Unauthorized(ApiResponse<Object>.Fail("Check your password and try again."));

            var token = _tokenService.CreateToken(user);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("auth", token, cookieOptions);
            return Ok(ApiResponse<Object>.Ok(null, "Successfully Logged In."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<Object>.Fail(ex.Message));
        }
    }

    [HttpGet("zero")]
    public async Task<ActionResult<ApiResponse<Object>>> Zero()
    {

        try
        {
            return Ok(ApiResponse<Object>.Ok(null, "Successfully Logged In."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<Object>.Fail(ex.Message));
        }
    }
}