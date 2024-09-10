﻿// <auto-generated />
using System;
using FastCleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace FastCleanArchitecture.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FastCleanArchitecture.Domain.TodoItems.TodoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<DateTimeOffset>("CreatedAtUtc")
                        .HasColumnType("TIMESTAMP(7) WITH TIME ZONE");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<Guid>("ListId")
                        .HasColumnType("RAW(16)");

                    b.Property<DateTimeOffset>("ModifiedAtUtc")
                        .HasColumnType("TIMESTAMP(7) WITH TIME ZONE");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Note")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("Priority")
                        .HasColumnType("NUMBER(10)");

                    b.Property<DateTime?>("Reminder")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)");

                    b.Property<Guid?>("TodoListId")
                        .HasColumnType("RAW(16)");

                    b.HasKey("Id");

                    b.HasIndex("TodoListId");

                    b.ToTable("TodoItems", (string)null);
                });

            modelBuilder.Entity("FastCleanArchitecture.Domain.TodoLists.TodoList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<DateTimeOffset>("CreatedAtUtc")
                        .HasColumnType("TIMESTAMP(7) WITH TIME ZONE");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTimeOffset>("ModifiedAtUtc")
                        .HasColumnType("TIMESTAMP(7) WITH TIME ZONE");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)");

                    b.HasKey("Id");

                    b.ToTable("TodoLists", (string)null);
                });

            modelBuilder.Entity("FastCleanArchitecture.Domain.TodoItems.TodoItem", b =>
                {
                    b.HasOne("FastCleanArchitecture.Domain.TodoLists.TodoList", null)
                        .WithMany("Items")
                        .HasForeignKey("TodoListId");
                });

            modelBuilder.Entity("FastCleanArchitecture.Domain.TodoLists.TodoList", b =>
                {
                    b.OwnsOne("FastCleanArchitecture.Domain.TodoLists.ValueObjects.Colour", "Colour", b1 =>
                        {
                            b1.Property<Guid>("TodoListId")
                                .HasColumnType("RAW(16)");

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnType("NVARCHAR2(2000)");

                            b1.HasKey("TodoListId");

                            b1.ToTable("TodoLists");

                            b1.WithOwner()
                                .HasForeignKey("TodoListId");
                        });

                    b.Navigation("Colour")
                        .IsRequired();
                });

            modelBuilder.Entity("FastCleanArchitecture.Domain.TodoLists.TodoList", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
