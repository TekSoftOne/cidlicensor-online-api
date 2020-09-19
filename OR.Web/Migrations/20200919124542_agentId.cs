using Microsoft.EntityFrameworkCore.Migrations;

namespace OR.Web.Migrations
{
    public partial class agentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgentId",
                table: "MembershipRequests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "MembershipRequests");
        }
    }
}
