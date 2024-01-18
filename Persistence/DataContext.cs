using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityAttendee> ActivityAttendees { get; set;}
        public DbSet<Photo> Photos { get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //many to many
            //ActivityAttendee ma klucz główny składający sie z dwóch kolumn
            builder.Entity<ActivityAttendee>()
                .HasKey(aa => new {aa.AppUserId, aa.ActivityId });

            // Ta konfiguracja określa relację wiele do jednego między encjami ActivityAttendee a AppUser.
            // ActivityAttendee ma jednego AppUser, a AppUser może mieć wiele Activities.
            builder.Entity<ActivityAttendee>()
                .HasOne(aa => aa.AppUser)
                .WithMany(au => au.Activities)
                .HasForeignKey(aa => aa.AppUserId);

            // Ta konfiguracja określa relację wiele do jednego między encjami ActivityAttendee a Activity.
            // ActivityAttendee ma jedno Activity, a Activity może mieć wiele Attendees.
            builder.Entity<ActivityAttendee>()
                .HasOne(aa => aa.Activity)
                .WithMany(a => a.Attendees)
                .HasForeignKey(aa => aa.ActivityId);
        }
    }
}
