﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using back_sistema_de_eventos.Context;

#nullable disable

namespace back_sistema_de_eventos.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("back_sistema_de_eventos.Models.App.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventDateTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdOrganizer")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("IdOrganizer");

                    b.HasIndex("Token")
                        .IsUnique();

                    b.ToTable("Events");
                });

            modelBuilder.Entity("back_sistema_de_eventos.Models.App.GuestRegistration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdInvitation")
                        .HasColumnType("int");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegisteredAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdInvitation")
                        .IsUnique();

                    b.HasIndex("IdUser");

                    b.ToTable("GuestRegistrations");
                });

            modelBuilder.Entity("back_sistema_de_eventos.Models.App.Invitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdEvent")
                        .HasColumnType("int");

                    b.Property<int?>("IdGuest")
                        .HasColumnType("int");

                    b.Property<DateTime>("InvitedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdEvent");

                    b.HasIndex("IdGuest");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("back_sistema_de_eventos.Models.App.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("back_sistema_de_eventos.Models.App.Event", b =>
                {
                    b.HasOne("back_sistema_de_eventos.Models.App.User", "Organizer")
                        .WithMany("OrganizedEvents")
                        .HasForeignKey("IdOrganizer")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("back_sistema_de_eventos.Models.App.GuestRegistration", b =>
                {
                    b.HasOne("back_sistema_de_eventos.Models.App.Invitation", "Invitation")
                        .WithOne("GuestRegistration")
                        .HasForeignKey("back_sistema_de_eventos.Models.App.GuestRegistration", "IdInvitation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("back_sistema_de_eventos.Models.App.User", "User")
                        .WithMany("GuestRegistrations")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Invitation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("back_sistema_de_eventos.Models.App.Invitation", b =>
                {
                    b.HasOne("back_sistema_de_eventos.Models.App.Event", "Event")
                        .WithMany("Invitations")
                        .HasForeignKey("IdEvent")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("back_sistema_de_eventos.Models.App.User", "Guest")
                        .WithMany("Invitations")
                        .HasForeignKey("IdGuest")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Event");

                    b.Navigation("Guest");
                });

            modelBuilder.Entity("back_sistema_de_eventos.Models.App.Event", b =>
                {
                    b.Navigation("Invitations");
                });

            modelBuilder.Entity("back_sistema_de_eventos.Models.App.Invitation", b =>
                {
                    b.Navigation("GuestRegistration")
                        .IsRequired();
                });

            modelBuilder.Entity("back_sistema_de_eventos.Models.App.User", b =>
                {
                    b.Navigation("GuestRegistrations");

                    b.Navigation("Invitations");

                    b.Navigation("OrganizedEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
