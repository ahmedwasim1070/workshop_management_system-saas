using Backend.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Builder 
// API map generator 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add the required things to understand controllers !
builder.Services.AddControllers();
// For Databse
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)
           .UseSnakeCaseNamingConvention());
// Custom Confgis
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage)
            .ToList();

        var errorString = string.Join("; ", errors);

        var response = ApiResponse<object>.Fail(errorString);

        return new BadRequestObjectResult(response);
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Scan files & folder to find routes
app.MapControllers();

app.Run();
