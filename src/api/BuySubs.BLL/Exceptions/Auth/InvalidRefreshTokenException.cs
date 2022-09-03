namespace BuySubs.BLL.Exceptions.Auth;

public sealed class InvalidRefreshTokenException : Exception
{
    public InvalidRefreshTokenException() : base($"Invalid refresh token.") { }
}