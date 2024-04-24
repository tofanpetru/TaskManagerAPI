using Core.GeneralUtilities;
using Core.Health;
using Core.Logging;
using Core.Swagger;
using Serilog;
using SwaggerHierarchySupport;

const string CorsPolicyName = "AllowAllOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddUtilities()
    .AddSwagger()
    //.AddAuthorization()
    .AddSerilogLogging(builder.Configuration)
    .AddCors(options =>
    {
        options.AddPolicy(name: CorsPolicyName,
                          builder =>
                          {
                              builder.AllowAnyOrigin();
                              builder.AllowAnyMethod();
                              builder.AllowAnyHeader();
                          });
    });

builder.Host.UseSerilog();

var app = builder.Build();

//Migrations

//Security
//todo 
//app.UseAuthentication();
//app.UseAuthorization();

//Endpoints

app.UseSwagger();
app.UseSwaggerUI(opts =>
{
    foreach (var groupName in app.DescribeApiVersions()
                                       .Select(desc => desc.GroupName))
    {
        opts.DisplayRequestDuration();
        opts.AddHierarchySupport();
        opts.SwaggerEndpoint(
            url: $"/swagger/{groupName}/swagger.json",
            name: groupName);
    }

    opts.RoutePrefix = string.Empty;
});

app.UseHealthCheckPaths();

app.UseCors(CorsPolicyName);

app.Run();