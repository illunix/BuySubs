using MediatR;
using Microsoft.AspNetCore.Http;

namespace BuySubs.BLL.Interfaces;

public interface IHttpRequest : IRequest<IResult>
{
}