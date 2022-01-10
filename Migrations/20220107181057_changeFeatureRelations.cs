using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carguero.FeatureFlag.Migrations
{
    public partial class changeFeatureRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FeaturesTenants",
                table: "FeaturesTenants");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "FeaturesTenants",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeaturesTenants",
                table: "FeaturesTenants",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Name",
                value: "FeatureA");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Name",
                value: "FeatureB");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Name",
                value: "FeatureC");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Name",
                value: "FeatureD");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesTenants_TenantId",
                table: "FeaturesTenants",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FeaturesTenants",
                table: "FeaturesTenants");

            migrationBuilder.DropIndex(
                name: "IX_FeaturesTenants_TenantId",
                table: "FeaturesTenants");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FeaturesTenants");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeaturesTenants",
                table: "FeaturesTenants",
                columns: new[] { "TenantId", "FeatureId" });

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Name",
                value: "Feature A");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Name",
                value: "Feature B");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Name",
                value: "Feature C");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Name",
                value: "Feature D");
        }
    }
}
