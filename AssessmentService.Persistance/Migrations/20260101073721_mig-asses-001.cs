using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssessmentService.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class migasses001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_PsychologyTestQuestions_PsychologyTestQuestionId",
                table: "Advertisements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Advertisements",
                table: "Advertisements");

            migrationBuilder.RenameTable(
                name: "Advertisements",
                newName: "AnswerOption");

            migrationBuilder.RenameIndex(
                name: "IX_Advertisements_PsychologyTestQuestionId",
                table: "AnswerOption",
                newName: "IX_AnswerOption_PsychologyTestQuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswerOption",
                table: "AnswerOption",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ExceptionLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExceptionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MethodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TraceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HttpContextUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HttpContextRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InnerException = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    StatusCode = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TraceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HttpContextUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HttpContextRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HttpContextResponse = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerOption_PsychologyTestQuestions_PsychologyTestQuestionId",
                table: "AnswerOption",
                column: "PsychologyTestQuestionId",
                principalTable: "PsychologyTestQuestions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerOption_PsychologyTestQuestions_PsychologyTestQuestionId",
                table: "AnswerOption");

            migrationBuilder.DropTable(
                name: "ExceptionLogs");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerOption",
                table: "AnswerOption");

            migrationBuilder.RenameTable(
                name: "AnswerOption",
                newName: "Advertisements");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerOption_PsychologyTestQuestionId",
                table: "Advertisements",
                newName: "IX_Advertisements_PsychologyTestQuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Advertisements",
                table: "Advertisements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_PsychologyTestQuestions_PsychologyTestQuestionId",
                table: "Advertisements",
                column: "PsychologyTestQuestionId",
                principalTable: "PsychologyTestQuestions",
                principalColumn: "Id");
        }
    }
}
