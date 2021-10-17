﻿//// <auto-generated />
//using System;
//using EventsApp.Persistence.Context;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

//namespace EventsApp.Persistence
//{
//    [DbContext(typeof(ApplicationDbContext))]
//    partial class ApplicationUserDbContextModelSnapshot : ModelSnapshot
//    {
//        protected override void BuildModel(ModelBuilder modelBuilder)
//        {
//#pragma warning disable 612, 618
//            modelBuilder
//                .HasAnnotation("Relational:MaxIdentifierLength", 128)
//                .HasAnnotation("ProductVersion", "5.0.10")
//                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

//            modelBuilder.Entity("EventsApp.Domain.Event", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int")
//                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

//                    b.Property<string>("Address")
//                        .IsRequired()
//                        .HasMaxLength(40)
//                        .HasColumnType("nvarchar(40)");

//                    b.Property<DateTime>("CreatedAt")
//                        .HasColumnType("datetime");

//                    b.Property<string>("Description")
//                        .HasMaxLength(300)
//                        .HasColumnType("nvarchar(300)");

//                    b.Property<string>("ImageUrl")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<bool>("IsActive")
//                        .HasColumnType("bit");

//                    b.Property<bool>("IsArchived")
//                        .HasColumnType("bit");

//                    b.Property<DateTime>("PlannedAt")
//                        .HasColumnType("datetime");

//                    b.Property<string>("Title")
//                        .IsRequired()
//                        .HasMaxLength(40)
//                        .HasColumnType("nvarchar(40)");

//                    b.Property<string>("UserId")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(450)");

//                    b.HasKey("Id");

//                    b.HasIndex("Id")
//                        .IsUnique();

//                    b.HasIndex("UserId");

//                    b.ToTable("Events");
//                });

//            modelBuilder.Entity("EventsApp.Domain.POCO.ApiUser", b =>
//                {
//                    b.Property<string>("Id")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("Email")
//                        .IsRequired()
//                        .HasMaxLength(50)
//                        .HasColumnType("nvarchar(50)");

//                    b.Property<string>("FirstName")
//                        .IsRequired()
//                        .HasMaxLength(40)
//                        .HasColumnType("nvarchar(40)");

//                    b.Property<string>("LastName")
//                        .IsRequired()
//                        .HasMaxLength(60)
//                        .HasColumnType("nvarchar(60)");

//                    b.Property<string>("UserName")
//                        .IsRequired()
//                        .HasMaxLength(50)
//                        .HasColumnType("nvarchar(50)");

//                    b.HasKey("Id");

//                    b.HasIndex("Id")
//                        .IsUnique();

//                    b.ToTable("AspNetUsers", "Identity");
//                });

//            modelBuilder.Entity("EventsApp.Domain.POCO.AppConfig", b =>
//                {
//                    b.Property<string>("Setting")
//                        .HasMaxLength(200)
//                        .HasColumnType("nvarchar(200)");

//                    b.Property<string>("Description")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<int>("Value")
//                        .HasColumnType("int");

//                    b.HasKey("Setting");

//                    b.HasIndex("Setting")
//                        .IsUnique();

//                    b.ToTable("AppConfigs");
//                });

//            modelBuilder.Entity("EventsApp.Domain.Event", b =>
//                {
//                    b.HasOne("EventsApp.Domain.POCO.ApiUser", "User")
//                        .WithMany("Events")
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.Navigation("User");
//                });

//            modelBuilder.Entity("EventsApp.Domain.POCO.ApiUser", b =>
//                {
//                    b.Navigation("Events");
//                });
//#pragma warning restore 612, 618
//        }
//    }
//}