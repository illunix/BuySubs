namespace BuySubs.BLL.Exceptions.Discounts;

public sealed class DiscountIsAlreadyActiveException : Exception
{
    public DiscountIsAlreadyActiveException() : base($"This discount is already active.") { }
}