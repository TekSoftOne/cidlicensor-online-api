using Microsoft.EntityFrameworkCore.Migrations;

namespace OR.Web.Migrations
{
    public partial class file_url : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorizationLetter",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "EmiratesIdBack",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "EmiratesIdFront",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "PassportAttachement",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "ProfilePhoto",
                table: "MembershipRequests");

            migrationBuilder.AddColumn<string>(
                name: "AuthorizationLetterUrl",
                table: "MembershipRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmiratesIdBackUrl",
                table: "MembershipRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmiratesIdFrontUrl",
                table: "MembershipRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportAttachementUrl",
                table: "MembershipRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhotoUrl",
                table: "MembershipRequests",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorizationLetterUrl",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "EmiratesIdBackUrl",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "EmiratesIdFrontUrl",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "PassportAttachementUrl",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "ProfilePhotoUrl",
                table: "MembershipRequests");

            migrationBuilder.AddColumn<string>(
                name: "AuthorizationLetter",
                table: "MembershipRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmiratesIdBack",
                table: "MembershipRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmiratesIdFront",
                table: "MembershipRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportAttachement",
                table: "MembershipRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhoto",
                table: "MembershipRequests",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }
    }
}
