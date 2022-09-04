namespace BuySubs.BLL.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(string userId);
    string GenerateRefreshToken();
}