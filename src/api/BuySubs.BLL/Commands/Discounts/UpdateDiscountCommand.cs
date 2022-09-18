using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.Commands.Discounts;

public readonly record struct UpdateDiscountCommand(
    Guid Id,
    string Name,
    double Value,
    bool IsActive
) : IHttpRequest;