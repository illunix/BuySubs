namespace BuySubs.BLL.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(Guid userId);
    string GenerateRefreshToken();
}