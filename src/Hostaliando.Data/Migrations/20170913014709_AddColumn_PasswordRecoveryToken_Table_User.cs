﻿//<auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hostaliando.Data.Migrations
{
    public partial class AddColumn_PasswordRecoveryToken_Table_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordRecoveryToken",
                table: "Users",
                type: "varchar(40)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordRecoveryToken",
                table: "Users");
        }
    }
}
