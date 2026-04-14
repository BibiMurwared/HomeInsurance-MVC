namespace HomeInsurance_MVC.Middleware
{
    using HomeInsurance_MVC.Services;

    /// <summary>
    /// Logs unhandled exceptions to database, then rethrows.
    /// </summary>
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogService logService)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await logService.LogSystemErrorAsync(
                    context.Request.Path,
                    exception.Message,
                    exception.StackTrace);

                throw;
            }

            return;
        }
    }
}
