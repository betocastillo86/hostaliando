﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hostaliando.Data.Migrations
{
    public partial class AddColumn_Color_Icon_Table_BookingSources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "BookingSources",
                maxLength: 7,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "BookingSources",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "BookingSources");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "BookingSources");
        }
    }
}
