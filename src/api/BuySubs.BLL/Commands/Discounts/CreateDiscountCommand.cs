using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.Commands.Discounts;

public readonly record struct CreateDiscountCommand(
    string Name,
    double Value,
    bool IsActive
) : IHttpRequest;