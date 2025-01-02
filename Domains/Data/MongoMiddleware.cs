using Domains.Data;
using Domains.ViewModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class MongoMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<MongoMiddleware> _logger;

    public MongoMiddleware(RequestDelegate next, ILogger<MongoMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, MongoDbContext dbContext)
    {
        try
        {
            // Check database connection
            var isMongoConnected = dbContext.GetMongoCollection<users>("users").Database.Client.Cluster.Description.State != MongoDB.Driver.Core.Clusters.ClusterState.Disconnected;
            if (!isMongoConnected)
            {
                _logger.LogError("MongoDB connection is not available.");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("MongoDB is unavailable.");
                return;
            }

            // Proceed to the next middleware
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "MongoDB connection error.");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("An error occurred while connecting to MongoDB.");
        }
    }
}
