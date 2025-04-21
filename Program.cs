using Microsoft.EntityFrameworkCore;
using OneIncUserAPI.Core.Application.Interfaces;
using OneIncUserAPI.Core.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MemoryAppDB>(opt => opt.UseInMemoryDatabase("InMemoryDB"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddTransient(typeof(IApplicationRepository<>), typeof(ApplicationRepository<>));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Add the exception handling middleware
app.UseMiddleware<ErrorMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("../swagger/v1/swagger.json", "UserAPI v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
