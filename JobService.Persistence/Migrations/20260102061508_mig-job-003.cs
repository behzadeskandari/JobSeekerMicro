using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class migjob003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Cities_CityLabel",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_CityLabel",
                table: "Job");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cities",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CityLabel",
                table: "Job");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cities",
                table: "Cities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Job_CityId",
                table: "Job",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Cities_CityId",
                table: "Job",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Cities_CityId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_CityId",
                table: "Job");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cities",
                table: "Cities");

            migrationBuilder.AddColumn<string>(
                name: "CityLabel",
                table: "Job",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Cities",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cities",
                table: "Cities",
                column: "Label");

            migrationBuilder.CreateIndex(
                name: "IX_Job_CityLabel",
                table: "Job",
                column: "CityLabel");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Cities_CityLabel",
                table: "Job",
                column: "CityLabel",
                principalTable: "Cities",
                principalColumn: "Label");
        }
    }
}
