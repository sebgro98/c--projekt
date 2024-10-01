using AuthApi.Models;
using AuthApi.DTOs;
public interface IAuthService
{
    Task<(bool Success, string Message, User User)> RegisterUserAsync(User user);
    Task<string> AuthenticateAsync(UserSigninDto userSigninDto);
}
