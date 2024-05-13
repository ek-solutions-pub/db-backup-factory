namespace dbf_api;

public static class Middleware
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

    public static IApplicationBuilder AddVersionHeader(this IApplicationBuilder builder)
    {
        return builder.Use(async (context, next) =>
        {
            await next();
            //TODO: Read API Version from configuration or appropriate source
            context.Response.Headers.Append("Api-Version", "1.0");  
        });
    }




}
