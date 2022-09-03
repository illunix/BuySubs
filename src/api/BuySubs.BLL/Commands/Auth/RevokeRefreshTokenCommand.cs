using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.Commands.Auth;

internal readonly record struct RevokeRefreshTokenCommand(Guid UserId) : IHttpRequest;