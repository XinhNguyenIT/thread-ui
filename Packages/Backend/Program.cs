using System.Text;
using System.Text.Json.Serialization;
using Backend.Dataset;
using Backend.Dataset.Datas;
using Backend.Dataset.Interfaces;
using Backend.DTOs.Internals;
using Backend.Extensions;
using Backend.Infrastructure;
using Backend.Mappers;
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

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
});

builder.Services.AddJwtAuthentication(builder.Configuration);

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
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IGenericRepository<RefreshToken>, RefreshTokenRepository>();

//Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUrlService, UrlService>();

//Internal DTOs
builder.Services.AddScoped<UserContext>();

//Helper
builder.Services.AddScoped<UserMapper>();

var app = builder.Build();

if (await CliHandler.HandleAsync(app, args))
{
    return;
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseStaticFiles();

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<UserContextMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
