using Newtonsoft.Json;

namespace DraftSimulator;

public class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger _logger;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        string message = "Draft result has failed.";
        if (e is DraftException)
        {
            DraftException draftException = (DraftException)e;
            context.Response.StatusCode = (int)draftException.StatusCode;
            message = draftException.Message;
        }

        _logger.LogError(e, e.Message);
        await context.Response.WriteAsync(
            JsonConvert.SerializeObject(new { message = message }));
    }
}    