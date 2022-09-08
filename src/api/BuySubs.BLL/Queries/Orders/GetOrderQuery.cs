using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.Queries.Orders;

public readonly record struct GetOrderQuery(Guid Id) : IHttpRequest;