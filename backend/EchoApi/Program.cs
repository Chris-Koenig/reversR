using FluentValidation;
using Serilog;
using EchoApi.Models;
using EchoApi.Services;
using EchoApi.Validators;

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Use Serilog
builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddOpenApi();
builder.Services.AddScoped<IEchoService, EchoService>();
builder.Services.AddScoped<IValidator<EchoRequest>, EchoRequestValidator>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();

// Configure to run on port 5000
app.Urls.Clear();
app.Urls.Add("http://localhost:5000");

// Echo endpoint
app.MapPost("/api/echo", async (EchoRequest request, IEchoService echoService, IValidator<EchoRequest> validator) =>
{
    var validationResult = await validator.ValidateAsync(request);
    
    if (!validationResult.IsValid)
    {
        Log.Warning("Validation failed for echo request: {@Errors}", validationResult.Errors);
        return Results.BadRequest(new { errors = validationResult.Errors.Select(e => e.ErrorMessage) });
    }

    try
    {
        var response = echoService.ProcessEcho(request);
        Log.Information("Echo request processed successfully");
        return Results.Ok(response);
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Error processing echo request");
        return Results.Problem("An error occurred while processing your request");
    }
})
.WithName("Echo")
.WithOpenApi();

app.Run();
