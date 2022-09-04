using BuySubs.API.Enums;
using BuySubs.BLL.Exceptions;
using BuySubs.BLL.Exceptions.Auth;
using FluentValidation;
using System.Net;

namespace BuySubs.API.Extensions;

internal static class ExceptionFilterExtensions
{
    public static (
        HttpStatusCode statusCode,
        ErrorCode errorCode
    ) ParseException(this Exception exception)
    {
        return exception switch
        {
            NotFoundException _ => (
                HttpStatusCode.NotFound,
                ErrorCode.NotFound
            ),
            InvalidCredentialsException _ => (
                HttpStatusCode.Unauthorized, 
                ErrorCode.InvalidCredentials
            ),
            _ => (
                HttpStatusCode.InternalServerError,
                ErrorCode.General
            ),
        };
    }
}