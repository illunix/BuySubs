using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.Commands.Services;

public readonly record struct UpdateServiceCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    bool IsActive
) : IHttpRequest;