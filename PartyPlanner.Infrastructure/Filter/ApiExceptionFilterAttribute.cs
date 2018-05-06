using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PartyPlanner.Infrastructure.Filter
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public class UnhandledExceptionResponse
        {
            public string Message { get; set; }
        }

        private readonly IDictionary<Type, Func<ExceptionContext, Exception, IActionResult>> exceptionHandlers;

        public ApiExceptionFilterAttribute()
        {
            exceptionHandlers = new Dictionary<Type, Func<ExceptionContext, Exception, IActionResult>>
            {
                [typeof(ValidationException)] = HandleValidationException
            };
        }
        public override void OnException(Microsoft.AspNetCore.Mvc.Filters.ExceptionContext context)
        {
            var exception = context.Exception;

            var exceptionType = exception.GetType();
            if (!exceptionHandlers.ContainsKey(exceptionType))
            {
                var request = context.HttpContext.Request;
                var response = new UnhandledExceptionResponse
                {
                    Message = context.Exception.ToString()
                };
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Result = new JsonResult(response);
            }
            else
            {
                context.Result = exceptionHandlers[exceptionType](context, exception);
            }
            base.OnException(context);
        }


        private IActionResult HandleValidationException(ExceptionContext actionExecutedContext, Exception exception)
        {
            var validationException = exception as ValidationException;
            var errors =
                validationException.Errors.Select(error => new ValidationError(error.ErrorCode, error.ErrorMessage));
            actionExecutedContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return actionExecutedContext.Result = new JsonResult(new ApiValidationErrors(errors));
        }
    }
}
