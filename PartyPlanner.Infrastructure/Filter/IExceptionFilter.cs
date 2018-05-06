using Microsoft.AspNetCore.Mvc.Filters;

namespace PartyPlanner.Infrastructure.Filter
{
    public interface IExceptionFilter : IFilterMetadata
    {
        void OnException(ExceptionContext context);
    }
}
