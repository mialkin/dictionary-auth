using Dictionary.Auth.Api.Configurations;
using Dictionary.Auth.Api.Settings;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.WriteTo.Console();
});

var services = builder.Services;

services.ConfigureAuth(builder.Configuration);
services.AddControllers();
services.AddRouting(options => options.LowercaseUrls = true);

services.AddSwaggerGen(options =>
{
    options.DescribeAllParametersInCamelCase();
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Dictionary auth API", Version = "v1" });
});

services.ConfigureMediatr();
services.ConfigureAdminSettings();

var application = builder.Build();

application.UseRouting();
application.UseAuthentication();
application.UseAuthorization();

application.UseSerilogRequestLogging();

application.UseSwagger();
application.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
    options.DocumentTitle = "Dictionary auth API";
});

application.MapControllers();

application.Run();
