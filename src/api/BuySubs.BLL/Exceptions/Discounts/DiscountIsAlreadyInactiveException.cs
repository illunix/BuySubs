namespace BuySubs.BLL.Exceptions.Discounts;

public sealed class DiscountIsAlreadyInactiveException : Exception
{
    public DiscountIsAlreadyInactiveException() : base($"This discount is already inactive.") { }
}