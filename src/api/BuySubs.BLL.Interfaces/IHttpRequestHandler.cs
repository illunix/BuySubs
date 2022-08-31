using MediatR;
using Microsoft.AspNetCore.Http;

namespace BuySubs.BLL.Interfaces;

public interface IHttpRequestHandler<T> : IRequestHandler<T, IResult> where T : IHttpRequest
{
}
