using Microsoft.AspNetCore.Mvc;
using TrackFlow.Application.Dtos.Auth;
using TrackFlow.Application.Interfaces;
using TrackFlow.Domain.Common;
using TrackFlow.Domain.Entities;
using TrackFlow.Infrastructure.Data;

namespace TrackFlow.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly TrackFlowDbContext _context;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordHasher _passwordHasher;

    public AuthController(
        TrackFlowDbContext context,
        IJwtTokenService jwtTokenService,
        IPasswordHasher passwordHasher)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
        _passwordHasher = passwordHasher;
    }

    /// <summary>
    /// Login with email and password.
    /// Test with: admin@trackflow.local / Admin123!
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
        if (user == null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }

        if (!user.IsActive)
        {
            return Unauthorized(new { message = "User account is inactive" });
        }

        var (accessToken, refreshToken) = _jwtTokenService.GenerateTokenPair(user);

        var response = new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = 15 * 60, // 15 minutes in seconds
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role.ToString()
            }
        };

        return Ok(response);
    }

    /// <summary>
    /// Register a new user account.
    /// </summary>
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
    {
        if (_context.Users.Any(u => u.Email == request.Email))
        {
            return BadRequest(new { message = "Email already in use" });
        }

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PasswordHash = _passwordHasher.Hash(request.Password),
            Role = UserRole.Employee,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        var (accessToken, refreshToken) = _jwtTokenService.GenerateTokenPair(newUser);

        var response = new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = 15 * 60,
            User = new UserDto
            {
                Id = newUser.Id,
                Email = newUser.Email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Role = newUser.Role.ToString()
            }
        };

        return CreatedAtAction(nameof(Register), response);
    }
}
