using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.Queries.Discounts;

public readonly record struct GetDiscountQuery(Guid Id) : IHttpRequest;