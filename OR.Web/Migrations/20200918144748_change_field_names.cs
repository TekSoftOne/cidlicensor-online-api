using Microsoft.EntityFrameworkCore.Migrations;

namespace OR.Web.Migrations
{
    public partial class change_field_names : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MembershipRequests");

            migrationBuilder.RenameColumn(
                name: "EmiratesIdNumber",
                table: "MembershipRequests",
                newName: "EmiratesIDNumber");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "MembershipRequests",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "MembershipRequests",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "MembershipRequests");

            migrationBuilder.RenameColumn(
                name: "EmiratesIDNumber",
                table: "MembershipRequests",
                newName: "EmiratesIdNumber");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "MembershipRequests",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MembershipRequests",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }
    }
}
