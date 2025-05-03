using EventTrackingSystem.Api.Middleware;
using EventTrackingSystem.Application.Common.Configurations;
using EventTrackingSystem.Application.Common.DTOs;
using EventTrackingSystem.Application.Common.Interfaces;
using EventTrackingSystem.Application.Common.Mappings;
using EventTrackingSystem.Application.Common.SMTP;
using EventTrackingSystem.Domain.Entities;
using EventTrackingSystem.Infrastructure.Persistence;
using EventTrackingSystem.Infrastructure.Persistence.Repositories;
using EventTrackingSystem.Infrastructure.Persistence.Seeders;
using EventTrackingSystem.Infrastructure.Persistence.Services;
using EventTrackingSystem.Infrastructure.Persistence.Workers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));

builder.Services
    .AddIdentity<UserEntity, RoleEntity>(options =>
    {
        options.Stores.MaxLengthForKeys = 128;

        options.Password.RequiredLength = 6;
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var singinKey = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(
        builder.Configuration["Authentication:Jwt:SecretKey"]
            ?? throw new NullReferenceException("Authentication:Jwt:SecretKey")
    )
);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = singinKey,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
    });


builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 1048576000;
});

builder.Services.AddControllers();

builder.Services.AddTransient<IAppDbSeeder, AppDbSeeder>();
builder.Services.AddAutoMapper(typeof(AppMapProfile));
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddTransient<IImageService, ImageService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddTransient<IPaginationService<EventDto, EventFilterDto>, EventPaginationService>();

builder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
builder.Services.AddScoped<ISpecialtyService, SpecialtyService>();
builder.Services.AddTransient<IPaginationService<SpecialtyDto, SpecialtyFilterDto>, SpecialtyPaginationService>();

builder.Services.AddScoped<IPartnerRepository, PartnerRepository>();
builder.Services.AddScoped<IPartnerService, PartnerService>();
builder.Services.AddTransient<IPaginationService<PartnerDto, PartnerFilterDto>, PartnerPaginationService>();

builder.Services.AddScoped<IAchievementRepository, AchievementRepository>();
builder.Services.AddScoped<IAchievementService, AchievementService>();
builder.Services.AddTransient<IPaginationService<AchievementDto, AchievementFilterDto>, AchievementPaginationService>();

builder.Services.AddScoped<IGalleryRepository, GalleryRepository>();
builder.Services.AddScoped<IGalleryService, GalleryService>();
builder.Services.AddTransient<IPaginationService<GalleryDto, GalleryFilterDto>, GalleryPaginationService>();

builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.Configure<EventNotificationSettings>(builder.Configuration.GetSection("EventNotificationSettings"));
builder.Services.AddScoped<IEventNotificationService, EventNotificationService>();

builder.Services.AddHostedService<EventNotificationWorker>();

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(
     configuration => configuration
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
);

app.UseCustomExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();


string imagesDirPath = app.Services.GetRequiredService<IImageService>().ImagesDir;

if (!Directory.Exists(imagesDirPath))
{
    Directory.CreateDirectory(imagesDirPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesDirPath),
    RequestPath = "/images"
});

app.UseAuthorization();
app.MapControllers();

await using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope())
{
    await scope.ServiceProvider.GetRequiredService<IAppDbSeeder>().SeedAsync();
}

BotWorker botWorker = new BotWorker(app);

app.Run();

