using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.Commands.Discounts;

public readonly record struct DeleteDiscountCommand(Guid Id) : IHttpRequest;