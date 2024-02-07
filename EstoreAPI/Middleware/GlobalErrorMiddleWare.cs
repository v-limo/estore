namespace Backend.Middleware;

public class GlobalErrorMiddleWare(ILogger<GlobalErrorMiddleWare> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            logger.LogInformation($"Request: {context.Request.Method} : {context.Request.Path}");
            await next(context);
            logger.LogInformation($"Response: {context.Response.StatusCode}");
            logger.LogInformation($"Response: {context.Response.Body}");
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred");
            var problemDetails = new ProblemDetails
            {
                Type = "Error message",
                Title = "Error message",
                Status = StatusCodes.Status500InternalServerError,
                Detail = e.Message,
                Instance = context.TraceIdentifier
            };
            var jsonProblemDetails = JsonSerializer.Serialize(problemDetails);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(jsonProblemDetails);
        }
    }
}