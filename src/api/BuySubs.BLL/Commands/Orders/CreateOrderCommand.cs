using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.Commands.Orders;

public readonly record struct CreateOrderCommand(Guid ServiceId) : IHttpRequest;