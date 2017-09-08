﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Hostaliando.Data;

namespace Hostaliando.Data.Migrations
{
    [DbContext(typeof(HostaliandoContext))]
    [Migration("20170907234957_AddColumn_Color_Icon_Table_BookingSources")]
    partial class AddColumn_Color_Icon_Table_BookingSources
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hostaliando.Data.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comments");

                    b.Property<DateTime>("CreationDateUtc")
                        .HasColumnType("datetime");

                    b.Property<bool>("Deleted");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime");

                    b.Property<string>("GuestEmail")
                        .HasColumnType("varchar(150)");

                    b.Property<int?>("GuestLocationId");

                    b.Property<string>("GuestName")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<int>("RoomId");

                    b.Property<int>("SourceId");

                    b.Property<short>("StatusId");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("money");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("GuestLocationId");

                    b.HasIndex("RoomId");

                    b.HasIndex("SourceId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Hostaliando.Data.BookingSource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color")
                        .HasMaxLength(7);

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<string>("Icon")
                        .HasMaxLength(30);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("BookingSources");
                });

            modelBuilder.Entity("Hostaliando.Data.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("varchar(2)");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("Hostaliando.Data.EmailNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body")
                        .IsRequired();

                    b.Property<string>("Cc")
                        .HasColumnName("CC")
                        .HasMaxLength(500);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("ScheduledDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("SentDate")
                        .HasColumnType("datetime");

                    b.Property<short>("SentTries");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("ToName")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("EmailNotifications");
                });

            modelBuilder.Entity("Hostaliando.Data.Hostel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("CreationDateUtc")
                        .HasColumnType("datetime");

                    b.Property<int>("CurrencyId");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<int>("LocationId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(15)");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("LocationId");

                    b.ToTable("Hostels");
                });

            modelBuilder.Entity("Hostaliando.Data.HostelBookingSource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HostelId");

                    b.Property<int>("SourceId");

                    b.HasKey("Id");

                    b.HasIndex("HostelId");

                    b.HasIndex("SourceId");

                    b.ToTable("HostelBookingSources");
                });

            modelBuilder.Entity("Hostaliando.Data.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("ParentLocationId");

                    b.HasKey("Id");

                    b.HasIndex("ParentLocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Hostaliando.Data.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("FullMessage")
                        .IsRequired();

                    b.Property<string>("IpAddress")
                        .HasMaxLength(100);

                    b.Property<short>("LogLevelId");

                    b.Property<string>("PageUrl")
                        .HasMaxLength(500);

                    b.Property<string>("ShortMessage")
                        .IsRequired();

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Hostaliando.Data.Notification", b =>
                {
                    b.Property<int>("Id");

                    b.Property<bool>("Active");

                    b.Property<bool>("Deleted");

                    b.Property<string>("EmailHtml");

                    b.Property<string>("EmailSubject")
                        .HasMaxLength(500);

                    b.Property<bool>("IsEmail");

                    b.Property<bool>("IsSystem");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("SystemText")
                        .HasMaxLength(2000);

                    b.Property<string>("Tags")
                        .HasMaxLength(3000);

                    b.Property<DateTime?>("UpdateDate");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Hostaliando.Data.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<byte>("Beds");

                    b.Property<DateTime>("CreationDateUtc")
                        .HasColumnType("datetime");

                    b.Property<bool>("Deleted");

                    b.Property<int>("HostelId");

                    b.Property<bool>("IsPrivated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<short>("RoomTypeId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("HostelId");

                    b.HasIndex("UserId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Hostaliando.Data.SystemNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<bool>("Seen");

                    b.Property<string>("TargetUrl")
                        .IsRequired()
                        .HasColumnName("TargetURL")
                        .HasMaxLength(500);

                    b.Property<int?>("TriggerUserId");

                    b.Property<int>("UserId");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.HasIndex("TriggerUserId");

                    b.HasIndex("UserId");

                    b.ToTable("SystemNotifications");
                });

            modelBuilder.Entity("Hostaliando.Data.SystemSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("SystemSettings");
                });

            modelBuilder.Entity("Hostaliando.Data.TextResource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("LanguageId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("TextResources");
                });

            modelBuilder.Entity("Hostaliando.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Deleted");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<int?>("HostelId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<short>("RoleId");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("varchar(6)");

                    b.Property<short>("TimeZone");

                    b.HasKey("Id");

                    b.HasIndex("HostelId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Hostaliando.Data.Booking", b =>
                {
                    b.HasOne("Hostaliando.Data.Location", "GuestLocation")
                        .WithMany()
                        .HasForeignKey("GuestLocationId")
                        .HasConstraintName("FK_Bookings_Locations");

                    b.HasOne("Hostaliando.Data.Room", "Room")
                        .WithMany("Bookings")
                        .HasForeignKey("RoomId")
                        .HasConstraintName("FK_Bookings_Rooms");

                    b.HasOne("Hostaliando.Data.BookingSource", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId")
                        .HasConstraintName("FK_Bookings_BookingSources");

                    b.HasOne("Hostaliando.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Bookings_Users");
                });

            modelBuilder.Entity("Hostaliando.Data.Hostel", b =>
                {
                    b.HasOne("Hostaliando.Data.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .HasConstraintName("FK_Hostels_Currencies");

                    b.HasOne("Hostaliando.Data.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .HasConstraintName("FK_Hostels_Locations");
                });

            modelBuilder.Entity("Hostaliando.Data.HostelBookingSource", b =>
                {
                    b.HasOne("Hostaliando.Data.Hostel", "Hostel")
                        .WithMany("HostelBookingSources")
                        .HasForeignKey("HostelId")
                        .HasConstraintName("FK_HostelBookingSources_Hostels");

                    b.HasOne("Hostaliando.Data.BookingSource", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId")
                        .HasConstraintName("FK_HostelBookingSources_BookingSources");
                });

            modelBuilder.Entity("Hostaliando.Data.Location", b =>
                {
                    b.HasOne("Hostaliando.Data.Location", "ParentLocation")
                        .WithMany("ChildrenLocations")
                        .HasForeignKey("ParentLocationId")
                        .HasConstraintName("FK_Locations_Locations");
                });

            modelBuilder.Entity("Hostaliando.Data.Log", b =>
                {
                    b.HasOne("Hostaliando.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Log_User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hostaliando.Data.Room", b =>
                {
                    b.HasOne("Hostaliando.Data.Hostel", "Hostel")
                        .WithMany("Rooms")
                        .HasForeignKey("HostelId")
                        .HasConstraintName("FK_Rooms_Hostels");

                    b.HasOne("Hostaliando.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Rooms_Users");
                });

            modelBuilder.Entity("Hostaliando.Data.SystemNotification", b =>
                {
                    b.HasOne("Hostaliando.Data.User", "TriggerUser")
                        .WithMany()
                        .HasForeignKey("TriggerUserId")
                        .HasConstraintName("FK_SystemNotification_TriggerUser");

                    b.HasOne("Hostaliando.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_SystemNotification_User");
                });

            modelBuilder.Entity("Hostaliando.Data.User", b =>
                {
                    b.HasOne("Hostaliando.Data.Hostel", "Hostel")
                        .WithMany()
                        .HasForeignKey("HostelId")
                        .HasConstraintName("FK_Users_Hostels");
                });
        }
    }
}
