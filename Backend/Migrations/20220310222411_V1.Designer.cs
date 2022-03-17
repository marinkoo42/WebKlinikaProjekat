﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace ClinicWeb.Migrations
{
    [DbContext(typeof(ClinicWebContext))]
    [Migration("20220310222411_V1")]
    partial class V1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Grad", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("imeGrada")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Gradovi");
                });

            modelBuilder.Entity("Models.Klinika", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("gradID")
                        .HasColumnType("int");

                    b.Property<string>("nazivKlinike")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("gradID");

                    b.ToTable("Klinike");
                });

            modelBuilder.Entity("Models.KlinikaOdeljenje", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("klinikaID")
                        .HasColumnType("int");

                    b.Property<string>("lekar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("odeljenjeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("klinikaID");

                    b.HasIndex("odeljenjeID");

                    b.ToTable("KlinikeOdeljenja");
                });

            modelBuilder.Entity("Models.Odeljenje", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("nazivOdeljenja")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("opisOdeljenja")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("slikaOdeljenja")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Odeljenja");
                });

            modelBuilder.Entity("Models.Rezervacija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("KlinikaOdeljenjeID")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("termin")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("KlinikaOdeljenjeID");

                    b.ToTable("Rezervacije");
                });

            modelBuilder.Entity("Models.Klinika", b =>
                {
                    b.HasOne("Models.Grad", "grad")
                        .WithMany()
                        .HasForeignKey("gradID");

                    b.Navigation("grad");
                });

            modelBuilder.Entity("Models.KlinikaOdeljenje", b =>
                {
                    b.HasOne("Models.Klinika", "klinika")
                        .WithMany()
                        .HasForeignKey("klinikaID");

                    b.HasOne("Models.Odeljenje", "odeljenje")
                        .WithMany()
                        .HasForeignKey("odeljenjeID");

                    b.Navigation("klinika");

                    b.Navigation("odeljenje");
                });

            modelBuilder.Entity("Models.Rezervacija", b =>
                {
                    b.HasOne("Models.KlinikaOdeljenje", "KlinikaOdeljenje")
                        .WithMany()
                        .HasForeignKey("KlinikaOdeljenjeID");

                    b.Navigation("KlinikaOdeljenje");
                });
#pragma warning restore 612, 618
        }
    }
}
