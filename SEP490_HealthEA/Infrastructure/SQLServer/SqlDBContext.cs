using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        public DbSet<DocumentProfile> DocumentProfiles { get; set; }
        public DbSet<HealthProfile> HealthProfiles { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Image> Images { get; set; }

        /// <summary>
        /// config sqlserver    
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(local);Database=devEnv;TrustServerCertificate=True;Trusted_Connection=True;");
            } }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>()
                .Property(i => i.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<DocumentProfile>()
            .HasOne(d => d.PatientProfile)
            .WithMany(u => u.MedicalRecords)
            .HasForeignKey(d => d.PantientId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HealthProfile>()
                .HasOne(d => d.User)
                .WithMany(u => u.PatientProfiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);

        }



    }
}