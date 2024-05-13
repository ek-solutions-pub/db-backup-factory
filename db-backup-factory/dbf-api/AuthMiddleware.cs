namespace dbf_api;

public static class AuthMiddleware
{
    public static IApplicationBuilder UseApiKeyMiddleware(this IApplicationBuilder builder, string apiKeyHeader, string validApiKey)
    {
        return builder.Use(async (context, next) =>
        {
            if (!context.Request.Headers.TryGetValue(apiKeyHeader, out var extractedApiKey) ||
                extractedApiKey != validApiKey)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid API Key");
            }
            else
            {
                await next();
            }
        });
    }

}
