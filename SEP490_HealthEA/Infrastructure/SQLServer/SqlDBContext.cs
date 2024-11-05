
using Domain.Models.Entities;
using Domain.Models.Entities.YourNamespace.Models;
using Microsoft.EntityFrameworkCore;

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

        public virtual DbSet<DocumentProfile> DocumentProfiles { get; set; }

        public virtual DbSet<HealthProfile> HealthProfiles { get; set; }

        public virtual DbSet<InvalidatedToken> InvalidatedTokens { get; set; }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<UserEvent> UserEvents { get; set; }
        public virtual DbSet<Reminder> Reminders { get; set; }
        public virtual DbSet<Notice> Notices { get; set; }
        public virtual DbSet<DeviceTokenRequest> DeviceTokens { get; set; }
        
        public DbSet<Image> Images { get; set; }

		public DbSet<DailyMetric> DailyMetrics { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentProfile>(entity =>
            {
                entity.HasIndex(e => e.PantientId, "IX_DocumentProfiles_PantientId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.HealthProfile).WithMany(p => p.DocumentProfiles)
                    .HasForeignKey(d => d.PantientId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<HealthProfile>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_HealthProfiles_UserId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.User).WithMany(p => p.healthProfiles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HealthProfiles_User_UserId");
            });

            modelBuilder.Entity<InvalidatedToken>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__invalida__3213E83F8938BA78");

                entity.ToTable("invalidated_token");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");
                entity.Property(e => e.ExpriryTime)
                    .HasPrecision(6)
                    .HasColumnName("expriry_time");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK__user__B9BE370F7E22D744");

                entity.ToTable("user");

                entity.HasIndex(e => e.Username, "UK5c856itaihtmi69ni04cmpc4m").IsUnique();

                entity.HasIndex(e => e.Email, "UKhl4ga9r00rh51mdaf20hmnslt").IsUnique();

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("user_id");
                entity.Property(e => e.Dob).HasColumnName("dob");
                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("first_name");
                entity.Property(e => e.Gender).HasColumnName("gender");
                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("last_name");
                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");
                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("phone");
                entity.Property(e => e.Role)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("role");
                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("status");
                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

			modelBuilder.Entity<DailyMetric>()
				.HasOne(dm => dm.User)
				.WithMany(u => u.DailyMetrics)
				.HasForeignKey(dm => dm.UserId);

		}


    }
}