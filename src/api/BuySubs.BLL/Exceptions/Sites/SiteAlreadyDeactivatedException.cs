namespace BuySubs.BLL.Exceptions.Sites;

public sealed class SiteAlreadyDectivatedException : Exception
{
    public SiteAlreadyDectivatedException() : base($"This site is already deactivated.") { }
}