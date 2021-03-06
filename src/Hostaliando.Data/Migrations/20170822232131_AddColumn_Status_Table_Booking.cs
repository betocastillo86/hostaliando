﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hostaliando.Data.Migrations
{
    public partial class AddColumn_Status_Table_Booking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "StatusId",
                table: "Bookings",
                nullable: false,
                defaultValue: (short)1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Bookings");
        }
    }
}
