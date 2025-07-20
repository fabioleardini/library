using Library.Application.Interfaces;
using Library.Application.Services;
using Library.Infrastructure.Interfaces;
using Library.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

// Configure DbContext
builder.Services.AddDbContext<Library.Infrastructure.Data.LibraryDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configure OpenAPI
builder.Services.AddOpenApi("v1");

// Add a custom transformer to configure OpenAPI info
builder.Services.AddSingleton<OpenApiDocumentTransformer>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Royal Library API v1");
        options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
        options.DocumentTitle = "Royal Library API Documentation";
        options.EnableFilter();
        options.DisplayRequestDuration();
        options.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
        options.ConfigObject.AdditionalItems.Add("tryItOutEnabled", true);
    });
}

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
