namespace BuySubs.BLL.Exceptions.Sites;

public sealed class SiteAlreadyActivatedException : Exception
{
    public SiteAlreadyActivatedException() : base($"This site is already activated.") { }
}