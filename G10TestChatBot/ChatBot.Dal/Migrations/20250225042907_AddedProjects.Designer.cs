﻿// <auto-generated />
using System;
using ChatBot.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChatBot.Dal.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20250225042907_AddedProjects")]
    partial class AddedProjects
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ChatBot.Dal.Entites.BotUser", b =>
                {
                    b.Property<long>("BotUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("BotUserId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TelegramUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BotUserId");

                    b.HasIndex("TelegramUserId")
                        .IsUnique();

                    b.ToTable("BotUser", (string)null);
                });

            modelBuilder.Entity("ChatBot.Dal.Entites.Education", b =>
                {
                    b.Property<long>("EducationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("EducationId"));

                    b.Property<string>("Degree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Institution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("UserInfoId")
                        .HasColumnType("bigint");

                    b.HasKey("EducationId");

                    b.HasIndex("UserInfoId");

                    b.ToTable("Education", (string)null);
                });

            modelBuilder.Entity("ChatBot.Dal.Entites.Experience", b =>
                {
                    b.Property<long>("ExperienceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ExperienceId"));

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("UserInfoId")
                        .HasColumnType("bigint");

                    b.HasKey("ExperienceId");

                    b.HasIndex("UserInfoId");

                    b.ToTable("Experience", (string)null);
                });

            modelBuilder.Entity("ChatBot.Dal.Entites.Project", b =>
                {
                    b.Property<long>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ProjectId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndingTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartingTime")
                        .HasColumnType("datetime2");

                    b.Property<long>("UserInfoId")
                        .HasColumnType("bigint");

                    b.HasKey("ProjectId");

                    b.HasIndex("UserInfoId");

                    b.ToTable("Project", (string)null);
                });

            modelBuilder.Entity("ChatBot.Dal.Entites.Skill", b =>
                {
                    b.Property<long>("SkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("SkillId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProficiencyLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserInfoId")
                        .HasColumnType("bigint");

                    b.HasKey("SkillId");

                    b.HasIndex("UserInfoId");

                    b.ToTable("Skill", (string)null);
                });

            modelBuilder.Entity("ChatBot.Dal.Entites.UserInfo", b =>
                {
                    b.Property<long>("UserInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserInfoId"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("BotUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserInfoId");

                    b.HasIndex("BotUserId")
                        .IsUnique();

                    b.ToTable("UserInfo", (string)null);
                });

            modelBuilder.Entity("ChatBot.Dal.Entites.Education", b =>
                {
                    b.HasOne("ChatBot.Dal.Entites.UserInfo", "UserInfo")
                        .WithMany("Educations")
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("ChatBot.Dal.Entites.Experience", b =>
                {
                    b.HasOne("ChatBot.Dal.Entites.UserInfo", "UserInfo")
                        .WithMany("Experiences")
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("ChatBot.Dal.Entites.Project", b =>
                {
                    b.HasOne("ChatBot.Dal.Entites.UserInfo", "UserInfo")
                        .WithMany("Projects")
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("ChatBot.Dal.Entites.Skill", b =>
                {
                    b.HasOne("ChatBot.Dal.Entites.UserInfo", "UserInfo")
                        .WithMany("Skills")
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("ChatBot.Dal.Entites.UserInfo", b =>
                {
                    b.HasOne("ChatBot.Dal.Entites.BotUser", "BotUser")
                        .WithOne("UserInfo")
                        .HasForeignKey("ChatBot.Dal.Entites.UserInfo", "BotUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BotUser");
                });

            modelBuilder.Entity("ChatBot.Dal.Entites.BotUser", b =>
                {
                    b.Navigation("UserInfo")
                        .IsRequired();
                });

            modelBuilder.Entity("ChatBot.Dal.Entites.UserInfo", b =>
                {
                    b.Navigation("Educations");

                    b.Navigation("Experiences");

                    b.Navigation("Projects");

                    b.Navigation("Skills");
                });
#pragma warning restore 612, 618
        }
    }
}
