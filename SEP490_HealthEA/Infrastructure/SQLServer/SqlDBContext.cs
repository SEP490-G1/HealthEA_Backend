using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
        public DbSet<Person> Persons { get; set; }
        public DbSet<Room> Rooms { get; set; }
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
            modelBuilder.Entity<Person>()
            .HasOne(d => d.Room)
            .WithMany(u => u.Persons)
            .HasForeignKey(d => d.roomId);
        }


    }
}
