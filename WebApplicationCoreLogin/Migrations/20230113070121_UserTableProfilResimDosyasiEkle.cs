﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationCoreLogin.Migrations
{
    public partial class UserTableProfilResimDosyasiEkle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilResimDosyası",
                table: "User",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "user_1.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilResimDosyası",
                table: "User");
        }
    }
}
