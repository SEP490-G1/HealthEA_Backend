﻿// <auto-generated />
using System;
using Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(SqlDBContext))]
    partial class SqlDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Entities.DailyMetric", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("BloodSugar")
                        .HasColumnType("float");

                    b.Property<double?>("BodyTemperature")
                        .HasColumnType("float");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int?>("DiastolicBloodPressure")
                        .HasColumnType("int");

                    b.Property<int?>("HeartRate")
                        .HasColumnType("int");

                    b.Property<double?>("Height")
                        .HasColumnType("float");

                    b.Property<double?>("OxygenSaturation")
                        .HasColumnType("float");

                    b.Property<int?>("SystolicBloodPressure")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("DailyMetrics");
                });

            modelBuilder.Entity("Domain.Models.Entities.Doctor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClinicAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClinicCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HistoryOfWork")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfAppointments")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfVideoCalls")
                        .HasColumnType("int");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Domain.Models.Entities.DoctorReport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DoctorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ReportDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ReportedDoctorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ReporterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ResolvedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("ReportedDoctorId");

                    b.HasIndex("ReporterId");

                    b.ToTable("DoctorReports");
                });

            modelBuilder.Entity("Domain.Models.Entities.DocumentProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContentMedical")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PantientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "PantientId" }, "IX_DocumentProfiles_PantientId");

                    b.ToTable("DocumentProfiles");
                });

            modelBuilder.Entity("Domain.Models.Entities.HealthProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Residence")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SharedStatus")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "UserId" }, "IX_HealthProfiles_UserId");

                    b.ToTable("HealthProfiles");
                });

            modelBuilder.Entity("Domain.Models.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Domain.Models.Entities.InvalidatedToken", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("id");

                    b.Property<DateTime?>("ExpriryTime")
                        .HasPrecision(6)
                        .HasColumnType("datetime2(6)")
                        .HasColumnName("expriry_time");

                    b.HasKey("Id")
                        .HasName("PK__invalida__3213E83F8938BA78");

                    b.ToTable("invalidated_token", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.Property<DateOnly?>("Dob")
                        .HasColumnType("date")
                        .HasColumnName("dob");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("first_name");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit")
                        .HasColumnName("gender");

                    b.Property<string>("LastName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("phone");

                    b.Property<string>("Role")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("role");

                    b.Property<string>("Status")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("status");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("username");

                    b.HasKey("UserId")
                        .HasName("PK__user__B9BE370F7E22D744");

                    b.HasIndex(new[] { "Username" }, "UK5c856itaihtmi69ni04cmpc4m")
                        .IsUnique();

                    b.HasIndex(new[] { "Email" }, "UKhl4ga9r00rh51mdaf20hmnslt")
                        .IsUnique();

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Entities.DailyMetric", b =>
                {
                    b.HasOne("Domain.Models.Entities.User", "User")
                        .WithMany("DailyMetrics")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.Entities.Doctor", b =>
                {
                    b.HasOne("Domain.Models.Entities.User", "User")
                        .WithOne("Doctor")
                        .HasForeignKey("Domain.Models.Entities.Doctor", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.Entities.DoctorReport", b =>
                {
                    b.HasOne("Domain.Models.Entities.Doctor", null)
                        .WithMany("DoctorReports")
                        .HasForeignKey("DoctorId");

                    b.HasOne("Domain.Models.Entities.Doctor", "ReportedDoctor")
                        .WithMany()
                        .HasForeignKey("ReportedDoctorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Models.Entities.User", "Reporter")
                        .WithMany()
                        .HasForeignKey("ReporterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ReportedDoctor");

                    b.Navigation("Reporter");
                });

            modelBuilder.Entity("Domain.Models.Entities.DocumentProfile", b =>
                {
                    b.HasOne("Domain.Models.Entities.HealthProfile", "HealthProfile")
                        .WithMany("DocumentProfiles")
                        .HasForeignKey("PantientId")
                        .IsRequired();

                    b.Navigation("HealthProfile");
                });

            modelBuilder.Entity("Domain.Models.Entities.HealthProfile", b =>
                {
                    b.HasOne("Domain.Models.Entities.User", "User")
                        .WithMany("healthProfiles")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_HealthProfiles_User_UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.Entities.Doctor", b =>
                {
                    b.Navigation("DoctorReports");
                });

            modelBuilder.Entity("Domain.Models.Entities.HealthProfile", b =>
                {
                    b.Navigation("DocumentProfiles");
                });

            modelBuilder.Entity("Domain.Models.Entities.User", b =>
                {
                    b.Navigation("DailyMetrics");

                    b.Navigation("Doctor");

                    b.Navigation("healthProfiles");
                });
#pragma warning restore 612, 618
        }
    }
}
