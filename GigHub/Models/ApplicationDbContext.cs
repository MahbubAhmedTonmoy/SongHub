using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gig> Gigs { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Attendance> Attends { get; set; }

        public DbSet<Following> Followings { get; set; }


        public DbSet<Notification> Notifications { get; set; }


        public DbSet<UserNotification> UserNotifications { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                .HasRequired(a => a.Gig)
                .WithMany(g => g.Attendances)
                .WillCascadeOnDelete(false);

            //1 user many followers// each follower required followee
            modelBuilder.Entity<ApplicationUser>() 
                .HasMany( u=> u.Followers)
                .WithRequired(f => f.Followee) 
                .WillCascadeOnDelete(false);
            // user has many followees// each followee required follower
            modelBuilder.Entity<ApplicationUser>()
                .HasMany( u=> u.Followees)
                .WithRequired(f => f.Follower) 
                .WillCascadeOnDelete(false);
				
			
			//each userNotification has one and only one user
			// revers -> each user have many userNotification
            modelBuilder.Entity<UserNotification>()
                .HasRequired(n => n.User)
                .WithMany(u => u.UserNotifications)
                .WillCascadeOnDelete(false);
                
            base.OnModelCreating(modelBuilder);
        }
    }
}