﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SchoolSystem.Models
{
    public class SchoolDB: IdentityDbContext<ApplicationUser>
    {

        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Level> Levels { get; set; }
       

        public SchoolDB(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Holiday>()
            //                .Property(h => h.Status)
            //                .HasDefaultValueSql("Pending");

            //modelBuilder.Entity<Attendance>()
            //             .Property(p => p.AttendanceStatus)
            //             .HasDefaultValue("Absent");

            base.OnModelCreating(modelBuilder);
        }



    }
}
