using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymGo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _add_field_isDelet_membershiptype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MembershipTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MembershipTypes");
        }
    }
}
