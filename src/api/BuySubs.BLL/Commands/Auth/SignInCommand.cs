using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.Commands.Auth;

public readonly record struct SignInCommand(
    string Email,
    string Password
) : IHttpRequest;