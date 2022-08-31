using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.Commands.Services;

public readonly record struct DeleteServiceCommand(Guid Id) : IHttpRequest;