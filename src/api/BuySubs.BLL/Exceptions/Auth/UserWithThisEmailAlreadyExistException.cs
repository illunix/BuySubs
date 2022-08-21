namespace BuySubs.BLL.Exceptions.Auth;

public sealed class UserWithThisEmailAlreadyExistException : Exception
{
    public UserWithThisEmailAlreadyExistException() : base($"There is already user associated with this email.") { }
}