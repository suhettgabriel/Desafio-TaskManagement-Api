using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar a Inje��o de Depend�ncia (DI)

// Adicionar o DbContext usando SQL Server
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar a Unit of Work e os Servi�os
// Usamos AddScoped para que a inst�ncia seja a mesma durante um �nico request HTTP
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddControllers();

// 2. Adicionar Swagger/OpenAPI para documenta��o da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Task Management API",
        Version = "v1",
        Description = "Uma API para gerenciamento de tarefas desenvolvida em .NET 8 com Clean Architecture."
    });
});


var app = builder.Build();

// 3. Configurar o pipeline de requisi��es HTTP

// Usar Swagger em qualquer ambiente para facilitar testes
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Management API V1");
    c.RoutePrefix = string.Empty; // Acessar Swagger UI na raiz (http://localhost:port/)
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();