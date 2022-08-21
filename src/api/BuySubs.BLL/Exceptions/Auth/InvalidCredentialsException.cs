namespace BuySubs.BLL.Exceptions.Auth;

public sealed class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException() : base($"Invalid username or password.") { }
}