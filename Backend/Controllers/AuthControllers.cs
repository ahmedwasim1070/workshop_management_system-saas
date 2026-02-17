using System.Reflection.Metadata;
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
            var isUser = await _context.Admins.AnyAsync(u => u.Email == request.Email);
            if (isUser)
            {
                return Conflict(ApiResponse<Object>.Fail("Email already in use."));
            }

            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest(ApiResponse<Object>.Fail("Email already in use."));
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var newAdmin = new Admin
            {
                FullName = request.FullName,
                Email = request.Email,
                Password = hashedPassword
            };

            _context.Admins.Add(newAdmin);
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<Object>.Ok(null, "Successfully Created User."));
        }
        catch (Exception)
        {
            return StatusCode(500, ApiResponse<Object>.Fail("Unkown Server Error."));
        }
    }
}