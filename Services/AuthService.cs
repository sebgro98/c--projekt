using AuthApi.Data;
using AuthApi.DTOs;
using AuthApi.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context; // f�r att kunna anv�nda oss utav databas operationer
    private readonly IConfiguration _configuration; //f�r att anv�nda jwt secret fr�n appsettings

    public AuthService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<(bool Success, string Message, User User)> RegisterUserAsync(User user)
    {
        if (await _context.Users.AnyAsync(u => u.Email == user.Email))
        {
            return (false, "User with this email already exists.", null);
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return (true, "User registered successfully.", user);
    }

    public async Task<string> AuthenticateAsync(UserSigninDto userSigninDto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == userSigninDto.Email && u.Password == userSigninDto.Password);

        if (user == null)
        {
            return null; // eller kasta ett undantag
        }

        // H�mta rollen baserat p� RoleId
        var role = await _context.Roles.FindAsync(user.RoleId);
        if (role == null)
        {
            return null; // eller kasta ett undantag
        }

        // H�mta JWT Secret fr�n appsettings.json
        var jwtSecret = _configuration["JwtSettings:Secret"];
        var key = Encoding.UTF8.GetBytes(jwtSecret);

        // Generera en JWT-token
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // L�gg till user id i tokenet
            new Claim(ClaimTypes.Role, role.RoleName) // L�gg till rollen i tokenet
        }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}
