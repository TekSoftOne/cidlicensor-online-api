using Microsoft.EntityFrameworkCore.Migrations;

namespace OR.Web.Migrations
{
    public partial class profile_pic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePhotoUrl",
                table: "MembershipRequests");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePic",
                table: "MembershipRequests",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePic",
                table: "MembershipRequests");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhotoUrl",
                table: "MembershipRequests",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }
    }
}
