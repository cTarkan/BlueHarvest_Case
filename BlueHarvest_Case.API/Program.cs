using BlueHarvest_Case.API.Middlewares;
using BlueHarvest_Case.Application;
using BlueHarvest_Case.Application.Validators;
using FluentValidation;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<CreateAccountRequestValidator>();
// Add CORS policy
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowSwaggerUI", policy =>
	{
		//#TODO check localhost may cause security leak
		policy.WithOrigins("http://localhost:8081")
		  .AllowAnyHeader()
		  .AllowAnyMethod()
		  .AllowCredentials();
	});
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
	options.AddDocumentTransformer((document, context, cancellationToken) =>
	{
		document.Servers.Clear();

		document.Servers.Add(new OpenApiServer
		{
			Url = Environment.GetEnvironmentVariable("OPENAPI_SERVER_URL"),
			Description = "Configured runtime URL"
		});

		return Task.CompletedTask;
	});
});

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowSwaggerUI");
app.UseAuthorization();
app.MapControllers();

app.Run();

// Required for WebApplicationFactory<T> in integration tests
namespace BlueHarvest_Case.API { public partial class Program { } }