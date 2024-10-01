using AuthApi.Data;
using AuthApi.DTOs;
using AuthApi.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;

    public AuthService(ApplicationDbContext context)
    {
        _context = context;
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

        // Generera en JWT-token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("YourSuperSecretKeyMustBe32Bytes!!");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, role.RoleName) // L�gg till rollen i tokenet
        }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}
