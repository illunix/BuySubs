using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.Commands.Services;

public readonly record struct CreateServiceCommand(
    Guid SiteId,
    string Name,
    string Description,
    decimal Price
) : IHttpRequest;