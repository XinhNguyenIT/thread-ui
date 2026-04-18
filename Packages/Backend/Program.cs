using System.Text.Json.Serialization;
using Backend.Dataset;
using Backend.Dataset.Dev;
using Backend.Dataset.Interfaces;
using Backend.Dataset.Stag;
using Backend.DTOs.Internals;
using Backend.Extensions;
using Backend.Infrastructure;
using Backend.Middlewares;
using Backend.Models;
using Backend.Repositories;
using Backend.Repositories.Interfaces;
using Backend.Services;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ThreadDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ThreadDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

//Data seed
builder.Services.AddScoped<ISeedData, DevSeedData>();
builder.Services.AddScoped<ISeedData, StagSeedData>();

builder.Services.AddScoped<SeedDataFactory>();
builder.Services.AddScoped<IdentitySeeder>();
builder.Services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();

//Unit of work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Repositories
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGenericRepository<RefreshToken>, RefreshTokenRepository>();

//Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();

//Internal DTOs
builder.Services.AddScoped<UserContext>();

var app = builder.Build();

if (await CliHandler.HandleAsync(app, args))
{
    return;
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<UserContextMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
