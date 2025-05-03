using EventTrackingSystem.Application.Common.Interfaces;
using EventTrackingSystem.Domain.Constants;
using EventTrackingSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace EventTrackingSystem.Infrastructure.Persistence.Seeders;

public class AppDbSeeder(
    AppDbContext context,
    IConfiguration configuration,
    UserManager<UserEntity> userManager,
    RoleManager<RoleEntity> roleManager,
    IImageService imageService
    ) : IAppDbSeeder
{
    public async Task SeedAsync()
    {
        await context.Database.MigrateAsync();

        using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            if (!await context.UserRoles.AnyAsync())
                await CreateUserRolesAsync();

            if (!await context.Users.AnyAsync())
                await CreateUserAsync();

            if (!await context.Events.AnyAsync())
                await CreatePreviewsAsync();

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task CreatePreviewsAsync()
    {
        var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "Previews.json");
        if (!File.Exists(jsonPath)) return;

        var jsonData = await File.ReadAllTextAsync(jsonPath);
        var announcements = JsonSerializer.Deserialize<List<EventEntity>>(jsonData);

        if (announcements != null)
        {
            var random = new Random();

            // Отримуємо всі ролі
            var roles = await context.Roles.Select(r => r.Id).ToListAsync();

            foreach (var announcement in announcements)
            {
                announcement.Date = DateTime.UtcNow.AddDays(random.Next(1, 10));

                var organizer = await context.Users.FirstOrDefaultAsync();
                announcement.OrganizerId = organizer?.Id ?? 1;
                announcement.PreviewPhoto = await imageService.SaveImageFromUrlAsync("https://dummyimage.com/800x600/000/fff.png&text=Event");


                // Додаємо випадкові ролі (від 1 до 3)
                int rolesCount = random.Next(1, Math.Min(3, roles.Count) + 1);
                var randomRoles = roles.OrderBy(_ => Guid.NewGuid()).Take(rolesCount).ToList();

                announcement.RoleEvents = randomRoles.Select(roleId => new RoleEventEntity
                {
                    RoleId = roleId,
                    Event = announcement
                }).ToList();
            }

            await context.Events.AddRangeAsync(announcements);
            await context.SaveChangesAsync();
        }
    }


    private async Task CreateUserRolesAsync()
    {
        foreach (var roleName in Roles.All)
        {
            await roleManager.CreateAsync(new RoleEntity
            {
                Name = roleName
            });
        }
    }
    private async Task CreateUserAsync()
    {
        var user = new UserEntity
        {
            FirstName = "Super",
            LastName = "Admin",
            Email = configuration["Admin:Email"]
                ?? throw new NullReferenceException("Admin:Email"),
            UserName = "superadmin",
        };

        IdentityResult result = await userManager.CreateAsync(
            user,
            configuration["Admin:Password"]
                ?? throw new NullReferenceException("Admin:Password")
        );

        if (!result.Succeeded)
            throw new Exception("Error creating admin account");

        result = await userManager.AddToRoleAsync(user, Roles.Admin);

        if (!result.Succeeded)
            throw new Exception("Role assignment error");
    }
}

