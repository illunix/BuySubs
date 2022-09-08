using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.Commands.Orders;

public readonly record struct DeleteOrderCommand(Guid Id) : IHttpRequest;