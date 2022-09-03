namespace BuySubs.BLL.Exceptions.Auth;

public sealed class RefreshTokenAlreadyRevoked : Exception
{
    public RefreshTokenAlreadyRevoked() : base($"Refresh token was already revoked.") { }
}