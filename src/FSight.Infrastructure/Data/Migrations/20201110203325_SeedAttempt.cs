using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FSight.Infrastructure.Data.Migrations
{
    public partial class SeedAttempt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Developers_Projects_ProjectId",
                table: "Developers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "ProjectManagers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 20, 33, 25, 188, DateTimeKind.Utc).AddTicks(6540),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 877, DateTimeKind.Utc).AddTicks(8170));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ProjectManagers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 20, 33, 25, 188, DateTimeKind.Utc).AddTicks(6230),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 877, DateTimeKind.Utc).AddTicks(7910));

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Developers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Developers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 20, 33, 25, 187, DateTimeKind.Utc).AddTicks(9490),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 877, DateTimeKind.Utc).AddTicks(2990));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Developers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 20, 33, 25, 187, DateTimeKind.Utc).AddTicks(8650),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 877, DateTimeKind.Utc).AddTicks(2090));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 20, 33, 25, 186, DateTimeKind.Utc).AddTicks(7620),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 876, DateTimeKind.Utc).AddTicks(580));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 20, 33, 25, 179, DateTimeKind.Utc).AddTicks(1300),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 869, DateTimeKind.Utc).AddTicks(3580));

            migrationBuilder.AddForeignKey(
                name: "FK_Developers_Projects_ProjectId",
                table: "Developers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Developers_Projects_ProjectId",
                table: "Developers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "ProjectManagers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 877, DateTimeKind.Utc).AddTicks(8170),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 20, 33, 25, 188, DateTimeKind.Utc).AddTicks(6540));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ProjectManagers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 877, DateTimeKind.Utc).AddTicks(7910),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 20, 33, 25, 188, DateTimeKind.Utc).AddTicks(6230));

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Developers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Developers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 877, DateTimeKind.Utc).AddTicks(2990),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 20, 33, 25, 187, DateTimeKind.Utc).AddTicks(9490));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Developers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 877, DateTimeKind.Utc).AddTicks(2090),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 20, 33, 25, 187, DateTimeKind.Utc).AddTicks(8650));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 876, DateTimeKind.Utc).AddTicks(580),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 20, 33, 25, 186, DateTimeKind.Utc).AddTicks(7620));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 869, DateTimeKind.Utc).AddTicks(3580),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 20, 33, 25, 179, DateTimeKind.Utc).AddTicks(1300));

            migrationBuilder.AddForeignKey(
                name: "FK_Developers_Projects_ProjectId",
                table: "Developers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
