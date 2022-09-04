using BuySubs.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BuySubs.BLL.Commands.Auth;

public readonly record struct RefreshAccessTokenCommand(string RefreshToken) : IHttpRequest
{
    public string? CurrentUserId { get; init; }
}