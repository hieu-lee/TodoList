﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoList.Data;

namespace TodoList.Migrations
{
    [DbContext(typeof(TodoDbContext))]
    partial class TodoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("TodoList.Models.Account", b =>
                {
                    b.Property<string>("username")
                        .HasColumnType("TEXT");

                    b.Property<string>("password")
                        .HasColumnType("TEXT");

                    b.HasKey("username");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("TodoList.Models.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("logged")
                        .HasColumnType("INTEGER");

                    b.Property<string>("username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("TodoList.Models.ToDoItem", b =>
                {
                    b.Property<string>("ItemId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Completed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContentHeight")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeleteHeight")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Important")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastNotified")
                        .HasColumnType("TEXT");

                    b.Property<string>("Owner")
                        .HasColumnType("TEXT");

                    b.Property<string>("ParentListId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeCreate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("TimeRemind")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<string>("ToDoListListId")
                        .HasColumnType("TEXT");

                    b.HasKey("ItemId");

                    b.HasIndex("ToDoListListId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("TodoList.Models.ToDoList", b =>
                {
                    b.Property<string>("ListId")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeleteHeight")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ownerusername")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeCreate")
                        .HasColumnType("TEXT");

                    b.HasKey("ListId");

                    b.HasIndex("Ownerusername");

                    b.ToTable("Lists");
                });

            modelBuilder.Entity("TodoList.Models.ToDoItem", b =>
                {
                    b.HasOne("TodoList.Models.ToDoList", null)
                        .WithMany("Items")
                        .HasForeignKey("ToDoListListId");
                });

            modelBuilder.Entity("TodoList.Models.ToDoList", b =>
                {
                    b.HasOne("TodoList.Models.Account", "Owner")
                        .WithMany("lists")
                        .HasForeignKey("Ownerusername");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("TodoList.Models.Account", b =>
                {
                    b.Navigation("lists");
                });

            modelBuilder.Entity("TodoList.Models.ToDoList", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
