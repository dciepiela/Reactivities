﻿using Domain;
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
        public DbSet<Comment> Comments { get; set;}
        public DbSet<UserFollowing> UserFollowings { get; set;}

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


            builder.Entity<Comment>()
                .HasOne(c => c.Activity)
                .WithMany(a => a.Comments)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserFollowing>(b =>
            {
                b.HasKey(k => new { k.ObserverId, k.TargetId });

                b.HasOne(o => o.Observer)
                    .WithMany(f => f.Followings)
                    .HasForeignKey(o => o.ObserverId)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(o => o.Target)
                    .WithMany(f => f.Followers)
                    .HasForeignKey(o => o.TargetId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

        }
    }
}
