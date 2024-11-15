﻿// <auto-generated />
using System;
using EventManagerJH.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventManagerJH.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241104121904_UpdateSchema")]
    partial class UpdateSchema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventManagerJH.Models.Boodschap", b =>
                {
                    b.Property<int>("BoodschapID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BoodschapID"));

                    b.Property<int>("EvenementID")
                        .HasColumnType("int");

                    b.Property<string>("Item")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BoodschapID");

                    b.HasIndex("EvenementID");

                    b.ToTable("Boodschappen");
                });

            modelBuilder.Entity("EventManagerJH.Models.Evenement", b =>
                {
                    b.Property<int>("EvenementID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EvenementID"));

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Locatie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EvenementID");

                    b.ToTable("Evenementen");

                    b.HasData(
                        new
                        {
                            EvenementID = 1,
                            Beschrijving = "Groot feest",
                            Datum = new DateTime(2024, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Locatie = "Jeugdhuis",
                            Titel = "Koerrock"
                        },
                        new
                        {
                            EvenementID = 2,
                            Beschrijving = "Privé evenement",
                            Datum = new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Locatie = "Binnen",
                            Titel = "Verjaardag Casi"
                        });
                });

            modelBuilder.Entity("EventManagerJH.Models.Shift", b =>
                {
                    b.Property<int>("ShiftID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShiftID"));

                    b.Property<DateTime>("EindTijd")
                        .HasColumnType("datetime2");

                    b.Property<int>("EvenementID")
                        .HasColumnType("int");

                    b.Property<string>("ShiftOmschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTijd")
                        .HasColumnType("datetime2");

                    b.HasKey("ShiftID");

                    b.HasIndex("EvenementID");

                    b.ToTable("Shiften");
                });

            modelBuilder.Entity("EventManagerJH.Models.TodoItem", b =>
                {
                    b.Property<int>("TodoItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TodoItemID"));

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EvenementID")
                        .HasColumnType("int");

                    b.HasKey("TodoItemID");

                    b.HasIndex("EvenementID");

                    b.ToTable("ToDoItems");
                });

            modelBuilder.Entity("EventManagerJH.Models.Boodschap", b =>
                {
                    b.HasOne("EventManagerJH.Models.Evenement", "Evenement")
                        .WithMany("BoodschappenLijst")
                        .HasForeignKey("EvenementID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evenement");
                });

            modelBuilder.Entity("EventManagerJH.Models.Shift", b =>
                {
                    b.HasOne("EventManagerJH.Models.Evenement", "Evenement")
                        .WithMany("ShiftenLijst")
                        .HasForeignKey("EvenementID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evenement");
                });

            modelBuilder.Entity("EventManagerJH.Models.TodoItem", b =>
                {
                    b.HasOne("EventManagerJH.Models.Evenement", "Evenement")
                        .WithMany("ToDoLijst")
                        .HasForeignKey("EvenementID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evenement");
                });

            modelBuilder.Entity("EventManagerJH.Models.Evenement", b =>
                {
                    b.Navigation("BoodschappenLijst");

                    b.Navigation("ShiftenLijst");

                    b.Navigation("ToDoLijst");
                });
#pragma warning restore 612, 618
        }
    }
}
