using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SQLServer
{
    public class SqlDBContext : DbContext
    {
        public SqlDBContext()
        {
        }

        public SqlDBContext(DbContextOptions<SqlDBContext> options)
            : base(options)
        {

        }
        /// <summary>
        /// Create DB set for Entities
        /// </summary>
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<PatientProfile> PatientProfiles { get; set; }
        public DbSet<User> User { get; set; }

        /// <summary>
        /// config sqlserver    
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MedicalRecord>()
    .HasOne(d => d.PatientProfile)
    .WithMany(u => u.MedicalRecords)
    .HasForeignKey(d => d.PantientId)
    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PatientProfile>()
                .HasOne(d => d.User)
                .WithMany(u => u.PatientProfiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);

        }


    }
}
