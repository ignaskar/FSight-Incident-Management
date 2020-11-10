using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FSight.Infrastructure.Data.Migrations
{
    public partial class TicketConfigurationTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Developers_DeveloperId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "DeveloperId",
                table: "Tickets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "ProjectManagers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 877, DateTimeKind.Utc).AddTicks(8170),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 19, 44, 7, 760, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ProjectManagers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 877, DateTimeKind.Utc).AddTicks(7910),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 19, 44, 7, 760, DateTimeKind.Utc).AddTicks(3540));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Developers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 877, DateTimeKind.Utc).AddTicks(2990),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 19, 44, 7, 759, DateTimeKind.Utc).AddTicks(8670));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Developers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 877, DateTimeKind.Utc).AddTicks(2090),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 19, 44, 7, 759, DateTimeKind.Utc).AddTicks(7410));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 876, DateTimeKind.Utc).AddTicks(580),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 19, 44, 7, 758, DateTimeKind.Utc).AddTicks(7130));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 869, DateTimeKind.Utc).AddTicks(3580),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 19, 44, 7, 751, DateTimeKind.Utc).AddTicks(6860));

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Developers_DeveloperId",
                table: "Tickets",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Developers_DeveloperId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "DeveloperId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "ProjectManagers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 19, 44, 7, 760, DateTimeKind.Utc).AddTicks(3790),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 877, DateTimeKind.Utc).AddTicks(8170));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ProjectManagers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 19, 44, 7, 760, DateTimeKind.Utc).AddTicks(3540),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 877, DateTimeKind.Utc).AddTicks(7910));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Developers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 19, 44, 7, 759, DateTimeKind.Utc).AddTicks(8670),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 877, DateTimeKind.Utc).AddTicks(2990));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Developers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 19, 44, 7, 759, DateTimeKind.Utc).AddTicks(7410),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 877, DateTimeKind.Utc).AddTicks(2090));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 19, 44, 7, 758, DateTimeKind.Utc).AddTicks(7130),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 876, DateTimeKind.Utc).AddTicks(580));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 10, 19, 44, 7, 751, DateTimeKind.Utc).AddTicks(6860),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 10, 19, 48, 33, 869, DateTimeKind.Utc).AddTicks(3580));

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Developers_DeveloperId",
                table: "Tickets",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
