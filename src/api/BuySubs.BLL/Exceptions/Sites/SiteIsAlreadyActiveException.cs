namespace BuySubs.BLL.Exceptions.Sites;

public sealed class SiteIsAlreadyActiveException : Exception
{
    public SiteIsAlreadyActiveException() : base($"This site is already active.") { }
}