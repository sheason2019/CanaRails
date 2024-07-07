using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Admin.Infrastructure.Exceptions;

public class HttpStandardException(int statusCode, string msg) : Exception
{
  public int StatusCode
  {
    get => statusCode;

  }

  public string Msg
  {
    get => msg;
  }
}

public class HttpStandardExceptionFilter : IActionFilter, IOrderedFilter
{
  public int Order => int.MaxValue - 10;

  public void OnActionExecuting(ActionExecutingContext context) { }

  public void OnActionExecuted(ActionExecutedContext context)
  {
    if (context.Exception is HttpStandardException standardException)
    {
      context.Result = new ObjectResult(standardException.Msg)
      {
        StatusCode = standardException.StatusCode
      };

      context.ExceptionHandled = true;
    }
  }
}