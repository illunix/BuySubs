using BuySubs.BLL.Interfaces;

namespace BuySubs.BLL.Commands.Sites;

public readonly record struct CreateSiteCommand(string Name) : IHttpRequest;