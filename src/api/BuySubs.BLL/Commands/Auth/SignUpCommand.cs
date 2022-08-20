using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.Commands.Auth;

public readonly record struct SignUpCommand(
    string Email,
    string Password,
    string ConfirmPassword
) : IHttpRequest;