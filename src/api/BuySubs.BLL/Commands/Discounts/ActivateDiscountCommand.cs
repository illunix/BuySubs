using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.Commands.Discounts;

public readonly record struct ActivateDiscountCommand(string Name) : IHttpRequest;
