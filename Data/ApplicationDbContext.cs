using Microsoft.EntityFrameworkCore;
using Outdoors.ly.Models;

namespace Outdoors.ly.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityUser> ActivityUsers { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<NeededSupply> NeededSupplies { get; set; }
        public DbSet<NotificationSetting> NotificationSettings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite key for the join table
            modelBuilder.Entity<ActivityUser>()
                .HasKey(au => new { au.ActivityId, au.UserId });

            // Relationships
            modelBuilder.Entity<ActivityUser>()
                .HasOne(au => au.Activity)
                .WithMany(a => a.ActivityUsers)
                .HasForeignKey(au => au.ActivityId);

            modelBuilder.Entity<ActivityUser>()
                .HasOne(au => au.User)
                .WithMany(u => u.ActivityUsers)
                .HasForeignKey(au => au.UserId);

            modelBuilder.Entity<Activity>().HasData(
                new Activity
                {
                    Id = 1,
                    Name = "Camping Trip",
                    Venue = "Blue Hills National Park",
                    StartDate = new DateTime(2025, 10, 20),
                    StartTime = new TimeSpan(14, 0, 0), // 2:00 PM
                    Details = "Weekend camping with hiking and bonfire."
                },
                new Activity
                {
                    Id = 2,
                    Name = "Picnic Day",
                    Venue = "Greenwood Park",
                    StartDate = new DateTime(2025, 11, 5),
                    StartTime = new TimeSpan(11, 0, 0), // 11:00 AM
                    Details = "Family-friendly picnic with games and BBQ."
                }
            );
        }
    }
}
