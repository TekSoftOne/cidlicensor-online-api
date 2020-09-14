using Microsoft.EntityFrameworkCore.Migrations;

namespace OR.Web.Migrations
{
    public partial class typeOfRquest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "MembershipRequests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestCategory",
                table: "MembershipRequests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "RequestCategory",
                table: "MembershipRequests");
        }
    }
}
