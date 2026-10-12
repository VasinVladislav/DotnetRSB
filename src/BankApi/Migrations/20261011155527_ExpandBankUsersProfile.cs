using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApi.Migrations
{
    /// <inheritdoc />
    public partial class ExpandBankUsersProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "BankUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "BankUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasNoMiddleName",
                table: "BankUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "BankUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "BankUsers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "BankUsers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "BankUsers");

            migrationBuilder.DropColumn(
                name: "HasNoMiddleName",
                table: "BankUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "BankUsers");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "BankUsers");
        }
    }
}
