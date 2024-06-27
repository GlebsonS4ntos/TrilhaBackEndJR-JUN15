using Desafio.API.Database.Context;
using Desafio.API.Interfaces;
using Desafio.API.Middlewares;
using Desafio.API.Models.Entities;
using Desafio.API.Repositories;
using Microsoft.AspNetCore.Identity;
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

builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(config =>
{
    config.AddProfile<AutoMapperConfig>();
}).CreateMapper());

builder.Services.AddIdentity<User, IdentityRole<Guid>>(opt => opt.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<CodigoCertoContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
