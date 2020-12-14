﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Soaps.Model.Data;

namespace Soaps.Migrations
{
    [DbContext(typeof(MvcSoapContext))]
    partial class MvcSoapContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Soaps.Model.Soap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("SoapTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SoapTypeId");

                    b.ToTable("Soaps");
                });

            modelBuilder.Entity("Soaps.Model.SoapDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SoapId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SoapId");

                    b.ToTable("SoapDetails");
                });

            modelBuilder.Entity("Soaps.Model.SoapType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SoapTypes");
                });

            modelBuilder.Entity("Soaps.Model.Soap", b =>
                {
                    b.HasOne("Soaps.Model.SoapType", "SoapType")
                        .WithMany("Soaps")
                        .HasForeignKey("SoapTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SoapType");
                });

            modelBuilder.Entity("Soaps.Model.SoapDetail", b =>
                {
                    b.HasOne("Soaps.Model.Soap", "Soap")
                        .WithMany("SoapDetails")
                        .HasForeignKey("SoapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Soap");
                });

            modelBuilder.Entity("Soaps.Model.Soap", b =>
                {
                    b.Navigation("SoapDetails");
                });

            modelBuilder.Entity("Soaps.Model.SoapType", b =>
                {
                    b.Navigation("Soaps");
                });
#pragma warning restore 612, 618
        }
    }
}
