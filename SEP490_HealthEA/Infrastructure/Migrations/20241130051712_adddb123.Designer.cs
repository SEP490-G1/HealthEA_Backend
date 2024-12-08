﻿// <auto-generated />
using System;
using Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(SqlDBContext))]
    [Migration("20241130051712_adddb123")]
    partial class adddb123
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Entities.Appointment", b =>
                {
                    b.Property<Guid>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan?>("EndTime")
                        .HasColumnType("time");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AppointmentId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("UserId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Domain.Models.Entities.DailyMetric", b =>
                {
                    b.Property<Guid>("SelectedProfileId")
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

                    b.HasKey("SelectedProfileId");

                    b.HasIndex("UserId");

                    b.ToTable("DailyMetrics");
                });

            modelBuilder.Entity("Domain.Models.Entities.DeviceTokenRequest", b =>
                {
                    b.Property<Guid>("SelectedProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DeviceToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SelectedProfileId");

                    b.ToTable("DeviceTokens");
                });

            modelBuilder.Entity("Domain.Models.Entities.Doctor", b =>
                {
                    b.Property<Guid>("SelectedProfileId")
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

                    b.HasKey("SelectedProfileId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Domain.Models.Entities.DocumentProfile", b =>
                {
                    b.Property<Guid>("SelectedProfileId")
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

                    b.HasKey("SelectedProfileId");

                    b.HasIndex(new[] { "PantientId" }, "IX_DocumentProfiles_PantientId");

                    b.ToTable("DocumentProfiles");
                });

            modelBuilder.Entity("Domain.Models.Entities.Event", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<DateTime>("EventDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OriginalEventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RepeatEndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RepeatFrequency")
                        .HasColumnType("int");

                    b.Property<int>("RepeatInterval")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EventId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Domain.Models.Entities.HealthProfile", b =>
                {
                    b.Property<Guid>("SelectedProfileId")
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

                    b.HasKey("SelectedProfileId");

                    b.HasIndex(new[] { "UserId" }, "IX_HealthProfiles_UserId");

                    b.ToTable("HealthProfiles");
                });

            modelBuilder.Entity("Domain.Models.Entities.Image", b =>
                {
                    b.Property<int>("SelectedProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SelectedProfileId"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SelectedProfileId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Domain.Models.Entities.InvalidatedToken", b =>
                {
                    b.Property<string>("SelectedProfileId")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("id");

                    b.Property<DateTime?>("ExpriryTime")
                        .HasPrecision(6)
                        .HasColumnType("datetime2(6)")
                        .HasColumnName("expriry_time");

                    b.HasKey("SelectedProfileId")
                        .HasName("PK__invalida__3213E83F8938BA78");

                    b.ToTable("invalidated_token", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Entities.News", b =>
                {
                    b.Property<Guid>("SelectedProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("SelectedProfileId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("Domain.Models.Entities.Notice", b =>
                {
                    b.Property<Guid>("NoticeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RecipientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("NoticeId");

                    b.HasIndex("UserId");

                    b.ToTable("Notices");
                });

            modelBuilder.Entity("Domain.Models.Entities.Reminder", b =>
                {
                    b.Property<Guid>("ReminderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsSent")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OffsetUnit")
                        .HasColumnType("int");

                    b.Property<int>("ReminderOffset")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReminderTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ReminderId");

                    b.HasIndex("EventId");

                    b.ToTable("Reminders");
                });

            modelBuilder.Entity("Domain.Models.Entities.Schedule", b =>
                {
                    b.Property<Guid>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ScheduleId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("Domain.Models.Entities.TokenCallModel", b =>
                {
                    b.Property<int>("SelectedProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SelectedProfileId"));

                    b.Property<string>("CallerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("callerId");

                    b.Property<string>("TokenCall")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("tokenCall");

                    b.HasKey("SelectedProfileId");

                    b.ToTable("TokenCall");
                });

            modelBuilder.Entity("Domain.Models.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.Property<string>("CallerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("callerid");

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

                    b.Property<string>("TokenCall")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("tokencall");

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

            modelBuilder.Entity("Domain.Models.Entities.UserEvent", b =>
                {
                    b.Property<Guid>("UserEventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOrganizer")
                        .HasColumnType("bit");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserEventId");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("UserEvents");
                });

            modelBuilder.Entity("Domain.Models.Entities.UserReport", b =>
                {
                    b.Property<Guid>("SelectedProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReportDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ReportedId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ReporterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ResolvedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("SelectedProfileId");

                    b.HasIndex("ReporterId");

                    b.ToTable("UserReports");
                });

            modelBuilder.Entity("Domain.Models.Entities.Appointment", b =>
                {
                    b.HasOne("Domain.Models.Entities.Doctor", "Doctors")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Entities.User", "Users")
                        .WithMany("Appointments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctors");

                    b.Navigation("Users");
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

            modelBuilder.Entity("Domain.Models.Entities.Notice", b =>
                {
                    b.HasOne("Domain.Models.Entities.User", "Users")
                        .WithMany("Notices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.Models.Entities.Reminder", b =>
                {
                    b.HasOne("Domain.Models.Entities.Event", "Events")
                        .WithMany("Reminders")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Events");
                });

            modelBuilder.Entity("Domain.Models.Entities.Schedule", b =>
                {
                    b.HasOne("Domain.Models.Entities.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("Domain.Models.Entities.UserEvent", b =>
                {
                    b.HasOne("Domain.Models.Entities.Event", "Events")
                        .WithMany("UserEvents")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Entities.User", "Users")
                        .WithMany("UserEvents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Events");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.Models.Entities.UserReport", b =>
                {
                    b.HasOne("Domain.Models.Entities.User", "Reporter")
                        .WithMany()
                        .HasForeignKey("ReporterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Reporter");
                });

            modelBuilder.Entity("Domain.Models.Entities.Event", b =>
                {
                    b.Navigation("Reminders");

                    b.Navigation("UserEvents");
                });

            modelBuilder.Entity("Domain.Models.Entities.HealthProfile", b =>
                {
                    b.Navigation("DocumentProfiles");
                });

            modelBuilder.Entity("Domain.Models.Entities.User", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("DailyMetrics");

                    b.Navigation("Doctor");

                    b.Navigation("Notices");

                    b.Navigation("UserEvents");

                    b.Navigation("healthProfiles");
                });
#pragma warning restore 612, 618
        }
    }
}
