using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SampleAPI.Utility
{
    public class ExceptionHandler: IExceptionFilter
    {
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError($"An exception occurred: {context.Exception}");

            var result = new ObjectResult(new
            {
                StatusCode = 500,
                Message = "An unexpected error occurred. Please try again later.",
                Detailed = context.Exception.Message
            })
            {
                StatusCode = 500
            };

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
