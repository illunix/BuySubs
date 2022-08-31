using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.Commands.Sites;

internal readonly record struct UpdateSiteCommand(
    Guid Id,
    string Name,
    bool IsActive
) : IHttpRequest;