var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

const string DevCorsPolicy = "DevCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(DevCorsPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseCors(DevCorsPolicy);
}

app.MapGet("/api/hello", () => new { message = "Hello from ASP.NET Core!" })
    .WithName("GetHello");

app.MapGet("/health", () => Results.Ok(new { status = "healthy" }))
    .WithName("HealthCheck");

app.Run();
