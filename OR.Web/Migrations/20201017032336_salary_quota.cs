using Microsoft.EntityFrameworkCore.Migrations;

namespace OR.Web.Migrations
{
    public partial class salary_quota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "MembershipRequests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "monthlyQuotaId",
                table: "MembershipRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "monthlySalaryId",
                table: "MembershipRequests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "monthlyQuotaId",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "monthlySalaryId",
                table: "MembershipRequests");
        }
    }
}
