using Microsoft.EntityFrameworkCore.Migrations;

namespace OR.Web.Migrations
{
    public partial class gender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "MembershipRequests");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "MembershipRequests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "MembershipRequests");

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "MembershipRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
