﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TestProject.Db;

#nullable disable

namespace TestProject.Migrations
{
    [DbContext(typeof(NodesDbContext))]
    [Migration("20240813085242_AddedExceptionJournal")]
    partial class AddedExceptionJournal
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TestProject.Db.Entity.ExceptionRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ExceptionJournal");
                });

            modelBuilder.Entity("TestProject.Db.Entity.Node", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Nodes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("eec6b9b3-9485-43a4-8833-08b0c0f969b3"),
                            Name = "Root"
                        });
                });

            modelBuilder.Entity("TestProject.Db.Entity.ExceptionRecord", b =>
                {
                    b.OwnsOne("TestProject.Db.Entity.ExceptionData", "Data", b1 =>
                        {
                            b1.Property<Guid>("ExceptionRecordId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Message")
                                .HasColumnType("text");

                            b1.HasKey("ExceptionRecordId");

                            b1.ToTable("ExceptionJournal");

                            b1.ToJson("Data");

                            b1.WithOwner()
                                .HasForeignKey("ExceptionRecordId");
                        });

                    b.Navigation("Data");
                });

            modelBuilder.Entity("TestProject.Db.Entity.Node", b =>
                {
                    b.HasOne("TestProject.Db.Entity.Node", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("TestProject.Db.Entity.Node", b =>
                {
                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}
