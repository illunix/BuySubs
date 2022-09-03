using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.Commands.Auth;

internal readonly record struct RefreshAccessTokenCommand(
    Guid CurrentUserId, 
    string RefreshToken
) : IHttpRequest;