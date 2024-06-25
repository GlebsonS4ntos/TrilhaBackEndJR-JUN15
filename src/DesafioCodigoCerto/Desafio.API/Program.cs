using Desafio.API.Database.Context;
using Desafio.API.Interfaces;
using Desafio.API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CodigoCertoContext>(opt => 
    {
        opt.UseSqlite(builder.Configuration.GetConnectionString("DesafioCodigoCertoDb"));
    });

builder.Services.AddScoped<IRepositoryEmployee, RepositoryEmployee>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
