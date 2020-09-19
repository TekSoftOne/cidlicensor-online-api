using Microsoft.EntityFrameworkCore.Migrations;

namespace OR.Web.Migrations
{
    public partial class use_address_occupation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "MembershipRequests");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "MembershipRequests",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "MembershipRequests");

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "MembershipRequests",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }
    }
}
