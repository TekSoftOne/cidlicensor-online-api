using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OR.Web.Migrations
{
    public partial class memberships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorizationLetter",
                table: "MembershipRequests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDay",
                table: "MembershipRequests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "MembershipRequests",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmiratesIdBack",
                table: "MembershipRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmiratesIdFront",
                table: "MembershipRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmiratesIdNumber",
                table: "MembershipRequests",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "MembershipRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "MembershipRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NationId",
                table: "MembershipRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "MembershipRequests",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportAttachement",
                table: "MembershipRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportNumber",
                table: "MembershipRequests",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhoto",
                table: "MembershipRequests",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReligionId",
                table: "MembershipRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VisaResidency",
                table: "MembershipRequests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorizationLetter",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "BirthDay",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "EmiratesIdBack",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "EmiratesIdFront",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "EmiratesIdNumber",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "NationId",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "PassportAttachement",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "PassportNumber",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "ProfilePhoto",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "ReligionId",
                table: "MembershipRequests");

            migrationBuilder.DropColumn(
                name: "VisaResidency",
                table: "MembershipRequests");
        }
    }
}
