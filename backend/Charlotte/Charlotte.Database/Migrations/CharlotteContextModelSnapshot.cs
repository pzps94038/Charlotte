﻿// <auto-generated />
using System;
using Charlotte.DataBase.DbContextModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Charlotte.Database.Migrations
{
    [DbContext(typeof(CharlotteContext))]
    partial class CharlotteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Charlotte.Database.Model.Factory", b =>
                {
                    b.Property<int>("FactoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FactoryId"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FactoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.HasKey("FactoryId");

                    b.ToTable("Factory");
                });

            modelBuilder.Entity("Charlotte.DataBase.Model.LoginLog", b =>
                {
                    b.Property<int>("LoginLogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoginLogID"), 1L, 1);

                    b.Property<string>("Flag")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nchar(1)");

                    b.Property<DateTime>("LoginTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginLogID");

                    b.HasIndex("UserId");

                    b.ToTable("LoginLog");
                });

            modelBuilder.Entity("Charlotte.DataBase.Model.ManagerLoginLog", b =>
                {
                    b.Property<int>("LoginLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoginLogId"), 1L, 1);

                    b.Property<string>("Flag")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nchar(1)");

                    b.Property<DateTime>("LoginTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ManagerUserId")
                        .HasColumnType("int");

                    b.HasKey("LoginLogId");

                    b.HasIndex("ManagerUserId");

                    b.ToTable("ManagerLoginLog");
                });

            modelBuilder.Entity("Charlotte.DataBase.Model.ManagerMain", b =>
                {
                    b.Property<int>("ManagerUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ManagerUserId"), 1L, 1);

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Address")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("Date");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Flag")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nchar(1)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ManagerUserId");

                    b.HasIndex("Account");

                    b.HasIndex("RoleId");

                    b.ToTable("ManagerMain");
                });

            modelBuilder.Entity("Charlotte.DataBase.Model.ManagerRefreshTokenLog", b =>
                {
                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ManagerUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("RefreshToken", "ManagerUserId");

                    b.HasIndex("ManagerUserId");

                    b.ToTable("ManagerRefreshTokenLog");
                });

            modelBuilder.Entity("Charlotte.Database.Model.ManagerRole", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("RoleId");

                    b.ToTable("ManagerRole");
                });

            modelBuilder.Entity("Charlotte.Database.Model.ManagerRoleAuth", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("RouterId")
                        .HasColumnType("int");

                    b.Property<string>("CreateAuth")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nchar(1)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeleteAuth")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nchar(1)");

                    b.Property<string>("ExportAuth")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nchar(1)");

                    b.Property<string>("ModifyAuth")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nchar(1)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ViewAuth")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nchar(1)");

                    b.HasKey("RoleId", "RouterId");

                    b.HasIndex("RouterId");

                    b.ToTable("ManagerRoleAuth");
                });

            modelBuilder.Entity("Charlotte.Database.Model.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Charlotte.DataBase.Model.OrderDetail", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductAmount")
                        .HasColumnType("int");

                    b.Property<int>("ProductPrice")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("Charlotte.DataBase.Model.ProductInformation", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<int>("CostPrice")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FactoryId")
                        .HasColumnType("int");

                    b.Property<int>("Inventory")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductImgPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductTypeId")
                        .HasColumnType("int");

                    b.Property<int>("SellPrice")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("FactoryId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("ProductInformation");
                });

            modelBuilder.Entity("Charlotte.DataBase.Model.ProductType", b =>
                {
                    b.Property<int>("ProductTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductTypeId"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Icon")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductTypeId");

                    b.ToTable("ProductType");
                });

            modelBuilder.Entity("Charlotte.DataBase.Model.RefreshTokenLog", b =>
                {
                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("RefreshToken", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokenLog");
                });

            modelBuilder.Entity("Charlotte.Database.Model.Router", b =>
                {
                    b.Property<int>("RouterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RouterId"), 1L, 1);

                    b.Property<string>("Flag")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nchar(1)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Icon")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("RouterName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("RouterId");

                    b.ToTable("Router");
                });

            modelBuilder.Entity("Charlotte.DataBase.Model.UserMain", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Address")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("Date");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Flag")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nchar(1)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("UserId");

                    b.HasIndex("Account");

                    b.ToTable("UserMain");
                });

            modelBuilder.Entity("Charlotte.DataBase.Model.LoginLog", b =>
                {
                    b.HasOne("Charlotte.DataBase.Model.UserMain", "UserMain")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserMain");
                });

            modelBuilder.Entity("Charlotte.DataBase.Model.ManagerLoginLog", b =>
                {
                    b.HasOne("Charlotte.DataBase.Model.ManagerMain", "ManagerMain")
                        .WithMany()
                        .HasForeignKey("ManagerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ManagerMain");
                });

            modelBuilder.Entity("Charlotte.DataBase.Model.ManagerMain", b =>
                {
                    b.HasOne("Charlotte.Database.Model.ManagerRole", "ManagerRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ManagerRole");
                });

            modelBuilder.Entity("Charlotte.DataBase.Model.ManagerRefreshTokenLog", b =>
                {
                    b.HasOne("Charlotte.DataBase.Model.ManagerMain", "ManagerMain")
                        .WithMany()
                        .HasForeignKey("ManagerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ManagerMain");
                });

            modelBuilder.Entity("Charlotte.Database.Model.ManagerRoleAuth", b =>
                {
                    b.HasOne("Charlotte.Database.Model.ManagerRole", "ManagerRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Charlotte.Database.Model.Router", "Router")
                        .WithMany()
                        .HasForeignKey("RouterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ManagerRole");

                    b.Navigation("Router");
                });

            modelBuilder.Entity("Charlotte.Database.Model.Order", b =>
                {
                    b.HasOne("Charlotte.DataBase.Model.UserMain", "UserMain")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserMain");
                });

            modelBuilder.Entity("Charlotte.DataBase.Model.OrderDetail", b =>
                {
                    b.HasOne("Charlotte.DataBase.Model.ProductInformation", "ProductInformation")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductInformation");
                });

            modelBuilder.Entity("Charlotte.DataBase.Model.ProductInformation", b =>
                {
                    b.HasOne("Charlotte.Database.Model.Factory", "Factory")
                        .WithMany()
                        .HasForeignKey("FactoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Charlotte.DataBase.Model.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Factory");

                    b.Navigation("ProductType");
                });

            modelBuilder.Entity("Charlotte.DataBase.Model.RefreshTokenLog", b =>
                {
                    b.HasOne("Charlotte.DataBase.Model.UserMain", "UserMain")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserMain");
                });
#pragma warning restore 612, 618
        }
    }
}
