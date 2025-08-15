using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymGo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _add_auditableEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MembershipTypes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "MembershipTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "MembershipTypes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "MembershipTypes",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MembershipTypes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "MembershipTypes");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "MembershipTypes");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "MembershipTypes");
        }
    }
}
