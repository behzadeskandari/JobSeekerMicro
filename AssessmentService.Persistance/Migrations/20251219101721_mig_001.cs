using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssessmentService.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig_001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MBTIResultAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MBTIResultAnswers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MBTIResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MBTIResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OccurredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Published = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalityTestResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpennessScore = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    ConscientiousnessScore = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    ExtraversionScore = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    AgreeablenessScore = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    NeuroticismScore = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CandidatesIds = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalityTestResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalityTraits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    TraitType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalityTraits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PsychologyTestResultAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    TotalScore = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResultData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTaken = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychologyTestResultAnswers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PsychologyTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTestAssignmentsIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychologyTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MBTIQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    MBTIResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MBTIResultAnswersId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MBTIQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MBTIQuestions_MBTIResultAnswers_MBTIResultAnswersId",
                        column: x => x.MBTIResultAnswersId,
                        principalTable: "MBTIResultAnswers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MBTIQuestions_MBTIResults_MBTIResultId",
                        column: x => x.MBTIResultId,
                        principalTable: "MBTIResults",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PersonalityTestItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonalityTraitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScoringDirection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    JobTestAssignmentsIds = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalityTestItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalityTestItems_PersonalityTraits_PersonalityTraitId",
                        column: x => x.PersonalityTraitId,
                        principalTable: "PersonalityTraits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PsychologyTestQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PsychologyTestId = table.Column<int>(type: "int", nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScoringWeight = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychologyTestQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PsychologyTestQuestions_PsychologyTests_PsychologyTestId",
                        column: x => x.PsychologyTestId,
                        principalTable: "PsychologyTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PsychologyTestResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PsychologyTestId = table.Column<int>(type: "int", nullable: false),
                    TotalScore = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResultData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTaken = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychologyTestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PsychologyTestResults_PsychologyTests_PsychologyTestId",
                        column: x => x.PsychologyTestId,
                        principalTable: "PsychologyTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalityTestResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalityTestItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Response = table.Column<int>(type: "int", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalityTestResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalityTestResponses_PersonalityTestItems_PersonalityTestItemId",
                        column: x => x.PersonalityTestItemId,
                        principalTable: "PersonalityTestItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Advertisements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PsychologyTestId = table.Column<int>(type: "int", nullable: false),
                    PsychologyTestQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertisements_PsychologyTestQuestions_PsychologyTestQuestionId",
                        column: x => x.PsychologyTestQuestionId,
                        principalTable: "PsychologyTestQuestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PsychologyTestResponseAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    PsychologyTestQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    PsychologyTestId = table.Column<int>(type: "int", nullable: true),
                    PsychologyTestResultAnswerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychologyTestResponseAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PsychologyTestResponseAnswers_PsychologyTestQuestions_PsychologyTestQuestionId",
                        column: x => x.PsychologyTestQuestionId,
                        principalTable: "PsychologyTestQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PsychologyTestResponseAnswers_PsychologyTestResultAnswers_PsychologyTestResultAnswerId",
                        column: x => x.PsychologyTestResultAnswerId,
                        principalTable: "PsychologyTestResultAnswers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PsychologyTestResponseAnswers_PsychologyTests_PsychologyTestId",
                        column: x => x.PsychologyTestId,
                        principalTable: "PsychologyTests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PsychologyTestInterpretation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PsychologyTestResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MinScore = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MaxScore = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Interpretation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PersonalityTestResultsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PsychologyTestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychologyTestInterpretation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PsychologyTestInterpretation_PersonalityTestResults_PersonalityTestResultsId",
                        column: x => x.PersonalityTestResultsId,
                        principalTable: "PersonalityTestResults",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PsychologyTestInterpretation_PsychologyTestResults_PsychologyTestResultId",
                        column: x => x.PsychologyTestResultId,
                        principalTable: "PsychologyTestResults",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PsychologyTestInterpretation_PsychologyTests_PsychologyTestId",
                        column: x => x.PsychologyTestId,
                        principalTable: "PsychologyTests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PsychologyTestResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PsychologyTestId = table.Column<int>(type: "int", nullable: false),
                    PsychologyTestQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychologyTestResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PsychologyTestResponses_PsychologyTestQuestions_PsychologyTestQuestionId",
                        column: x => x.PsychologyTestQuestionId,
                        principalTable: "PsychologyTestQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PsychologyTestResponses_PsychologyTestResults_TestResultId",
                        column: x => x.TestResultId,
                        principalTable: "PsychologyTestResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PsychologyTestResponses_PsychologyTests_PsychologyTestId",
                        column: x => x.PsychologyTestId,
                        principalTable: "PsychologyTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_PsychologyTestQuestionId",
                table: "Advertisements",
                column: "PsychologyTestQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_MBTIQuestions_MBTIResultAnswersId",
                table: "MBTIQuestions",
                column: "MBTIResultAnswersId");

            migrationBuilder.CreateIndex(
                name: "IX_MBTIQuestions_MBTIResultId",
                table: "MBTIQuestions",
                column: "MBTIResultId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalityTestItems_PersonalityTraitId",
                table: "PersonalityTestItems",
                column: "PersonalityTraitId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalityTestResponses_PersonalityTestItemId",
                table: "PersonalityTestResponses",
                column: "PersonalityTestItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychologyTestInterpretation_PersonalityTestResultsId",
                table: "PsychologyTestInterpretation",
                column: "PersonalityTestResultsId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychologyTestInterpretation_PsychologyTestId",
                table: "PsychologyTestInterpretation",
                column: "PsychologyTestId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychologyTestInterpretation_PsychologyTestResultId",
                table: "PsychologyTestInterpretation",
                column: "PsychologyTestResultId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychologyTestQuestions_PsychologyTestId",
                table: "PsychologyTestQuestions",
                column: "PsychologyTestId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychologyTestResponseAnswers_PsychologyTestId",
                table: "PsychologyTestResponseAnswers",
                column: "PsychologyTestId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychologyTestResponseAnswers_PsychologyTestQuestionId",
                table: "PsychologyTestResponseAnswers",
                column: "PsychologyTestQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychologyTestResponseAnswers_PsychologyTestResultAnswerId",
                table: "PsychologyTestResponseAnswers",
                column: "PsychologyTestResultAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychologyTestResponses_PsychologyTestId",
                table: "PsychologyTestResponses",
                column: "PsychologyTestId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychologyTestResponses_PsychologyTestQuestionId",
                table: "PsychologyTestResponses",
                column: "PsychologyTestQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychologyTestResponses_TestResultId",
                table: "PsychologyTestResponses",
                column: "TestResultId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychologyTestResults_PsychologyTestId",
                table: "PsychologyTestResults",
                column: "PsychologyTestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertisements");

            migrationBuilder.DropTable(
                name: "MBTIQuestions");

            migrationBuilder.DropTable(
                name: "OutboxMessage");

            migrationBuilder.DropTable(
                name: "PersonalityTestResponses");

            migrationBuilder.DropTable(
                name: "PsychologyTestInterpretation");

            migrationBuilder.DropTable(
                name: "PsychologyTestResponseAnswers");

            migrationBuilder.DropTable(
                name: "PsychologyTestResponses");

            migrationBuilder.DropTable(
                name: "MBTIResultAnswers");

            migrationBuilder.DropTable(
                name: "MBTIResults");

            migrationBuilder.DropTable(
                name: "PersonalityTestItems");

            migrationBuilder.DropTable(
                name: "PersonalityTestResults");

            migrationBuilder.DropTable(
                name: "PsychologyTestResultAnswers");

            migrationBuilder.DropTable(
                name: "PsychologyTestQuestions");

            migrationBuilder.DropTable(
                name: "PsychologyTestResults");

            migrationBuilder.DropTable(
                name: "PersonalityTraits");

            migrationBuilder.DropTable(
                name: "PsychologyTests");
        }
    }
}
