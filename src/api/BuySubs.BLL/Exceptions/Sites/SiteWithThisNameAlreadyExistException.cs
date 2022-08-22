namespace BuySubs.BLL.Exceptions.Sites;
public sealed class SiteWithThisNameAlreadyExistException : Exception
{
    public SiteWithThisNameAlreadyExistException() : base($"There is already site with this name.") { }
}