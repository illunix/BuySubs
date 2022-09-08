using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.Commands.Orders;

public readonly record struct UpdateOrderCommand(
    Guid Id,
    Guid ServiceId
) : IHttpRequest;