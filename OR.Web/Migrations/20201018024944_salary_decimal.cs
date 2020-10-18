using Microsoft.EntityFrameworkCore.Migrations;

namespace OR.Web.Migrations
{
    public partial class salary_decimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "monthlyQuotaId",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "monthlySalaryId",
                table: "MembershipRequests");

            migrationBuilder.AddColumn<decimal>(
                name: "monthlyQuota",
                table: "MembershipRequests",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "monthlySalary",
                table: "MembershipRequests",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "monthlyQuota",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "monthlySalary",
                table: "MembershipRequests");

            migrationBuilder.AddColumn<int>(
                name: "monthlyQuotaId",
                table: "MembershipRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "monthlySalaryId",
                table: "MembershipRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
