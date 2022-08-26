using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.Queries.Discounts;

internal readonly record struct GetDiscountQuery(string Name) : IHttpRequest;