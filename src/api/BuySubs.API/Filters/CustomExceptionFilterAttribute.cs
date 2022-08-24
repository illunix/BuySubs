using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BuySubs.API.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
internal sealed class CustomExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var (
            statusCode, 
            errorCode
        ) = context.Exception.ParseException();

        context.HttpContext.Response.ContentType = "application/json";
        context.HttpContext.Response.StatusCode = (int)statusCode;
        context.Result = new JsonResult(new
        {
            error = context.Exception.Message,
            code = errorCode
        });
    }
}
