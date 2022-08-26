namespace BuySubs.BLL.Exceptions.Sites;

public sealed class SiteIsAlreadyInactiveException : Exception
{
    public SiteIsAlreadyInactiveException() : base($"This site is already deactivated.") { }
}