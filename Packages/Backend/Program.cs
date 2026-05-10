using System.Text;
using System.Text.Json.Serialization;
using Backend.Background.Queue;
using Backend.Background.Workers;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        // Phẳng hóa các lỗi từ ModelState thành List<string>
        var errorList = actionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage)
            .ToList();

        var response = new Dictionary<string, object?>
        {
            ["success"] = false,
            ["statusCode"] = StatusCodes.Status400BadRequest,
            ["message"] = "Dữ liệu không hợp lệ",
            ["traceId"] = actionContext.HttpContext.TraceIdentifier,
            ["errors"] = errorList
        };

        return new BadRequestObjectResult(response);
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
    });
});

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
builder.Services.AddScoped<ISeedData, DefaultSeedData>();
builder.Services.AddScoped<ISeedData, TestSeedData>();

builder.Services.AddScoped<SeedDataFactory>();
builder.Services.AddScoped<IdentitySeeder>();
builder.Services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();

//Unit of work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Repositories
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IStoryRepository, StoryRepository>();
builder.Services.AddScoped<IMediaRepository, MediaRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

//Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ILikeService, LikeService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IUrlService, UrlService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMediaProcessor, MediaProcessor>();

//Internal DTOs
builder.Services.AddScoped<UserContext>();

//Helper
builder.Services.AddScoped<UserMapper>();
builder.Services.AddScoped<PostMapper>();
builder.Services.AddScoped<MediaMapper>();

builder.Services.AddSingleton<IMediaQueue, MediaQueue>();
builder.Services.AddHostedService<MediaWorker>();

var app = builder.Build();

if (await CliHandler.HandleAsync(app, args))
{
    return;
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

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
