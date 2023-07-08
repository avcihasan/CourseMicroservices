using Course.Gateway.DelegateHandlers;
using Microsoft.Extensions.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOcelot().AddDelegatingHandler<TokenExchangeDelegateHandler>();
builder.Services.AddHttpClient<TokenExchangeDelegateHandler>();
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddJsonFile($"configuration.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables();

builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme",options =>
{
    options.Authority = builder.Configuration["IdentityServerUrl"];
    options.Audience = "resource_gateway";
    options.RequireHttpsMetadata = false;
});


var app = builder.Build();

await app.UseOcelot();

app.Run();
