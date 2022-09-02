namespace BuySubs.BLL.Interfaces;

public interface IJwtHandler
{
    string GetAccessToken(string email);
}