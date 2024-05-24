using Microsoft.AspNetCore.Diagnostics;

namespace MagniseRecruitmentTask.Tools
{
    public class DefaultExceptionHadler : IExceptionHandler
    {
        private readonly ILogger<DefaultExceptionHadler> _logger;

        public DefaultExceptionHadler(ILogger<DefaultExceptionHadler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, exception.Message);

            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsJsonAsync("Something went wrong.");

            return true;
        }
    }
}
