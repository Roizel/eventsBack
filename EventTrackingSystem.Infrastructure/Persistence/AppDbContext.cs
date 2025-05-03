using EventTrackingSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventTrackingSystem.Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<UserEntity, RoleEntity, int,
   IdentityUserClaim<int>, UserRoleEntity, IdentityUserLogin<int>,
   IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<EventEntity> Events { get; set; }
    public DbSet<RoleEventEntity> RoleEvents { get; set; }
    public DbSet<MediaEntity> Media { get; set; }
    public DbSet<SpecialtyEntity> Specialties { get; set; }
    public DbSet<PartnerEntity> Partners { get; set; }
    public DbSet<AchievementEntity> Achievements { get; set; }
    public DbSet<GalleryEntity> Galleries { get; set; }
    public DbSet<GalleryPhotoEntity> GalleryPhotos { get; set; }
    public DbSet<EventNotificationLog> EventNotificationLogs { get; set; }
    public DbSet<TelegramChatEntity> TelegramChats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRoleEntity>(ur =>
        {
            ur.HasKey(ur => new { ur.UserId, ur.RoleId });
            ur.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(r => r.RoleId)
                .IsRequired();
            ur.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(u => u.UserId)
                .IsRequired();

            modelBuilder.Entity<RoleEventEntity>()
                .HasKey(re => new { re.RoleId, re.EventId });
            modelBuilder.Entity<RoleEventEntity>()
                .HasOne(re => re.Role)
                .WithMany(r => r.RoleEvents)
                .HasForeignKey(re => re.RoleId);
            modelBuilder.Entity<RoleEventEntity>()
                .HasOne(re => re.Event)
                .WithMany(e => e.RoleEvents)
                .HasForeignKey(re => re.EventId);

            modelBuilder.Entity<GalleryPhotoEntity>()
                .HasOne(gp => gp.Gallery)
                .WithMany(g => g.Photos)
                .HasForeignKey(gp => gp.GalleryId);
        });
    }
}
