using DirectoryService.Infrastructure.Postgres;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//AddDbContext or AddScoped
// ! в конце - для exception если не будет указана строка подключения
builder.Services.AddScoped<DirectoryServiceDbContext>(_ => 
    new DirectoryServiceDbContext(builder.Configuration.GetConnectionString("DsServiceDb")!));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "DirectoryService API v1"));
}

app.MapControllers();

app.Run();