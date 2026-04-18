using System.Text.Json.Serialization;
using Backend.Dataset;
using Backend.Dataset.Dev;
using Backend.Dataset.Interfaces;
using Backend.Dataset.Stag;
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

//Repositories
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Service
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

if (await CliHandler.HandleAsync(app, args))
{
    return;
}

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
