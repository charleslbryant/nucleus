using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nucleus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "workflow_run",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    platform = table.Column<string>(type: "text", nullable: false),
                    external_workflow_id = table.Column<string>(type: "text", nullable: false),
                    workflow_name = table.Column<string>(type: "text", nullable: false),
                    external_execution_id = table.Column<string>(type: "text", nullable: false),
                    session_id = table.Column<string>(type: "text", nullable: true),
                    triggered_by = table.Column<string>(type: "text", nullable: false),
                    mode = table.Column<string>(type: "text", nullable: false),
                    started_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    completed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    success = table.Column<bool>(type: "boolean", nullable: false),
                    error_message = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workflow_run", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "model_run",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    workflow_run_id = table.Column<Guid>(type: "uuid", nullable: false),
                    platform = table.Column<string>(type: "text", nullable: false),
                    external_node_id = table.Column<string>(type: "text", nullable: false),
                    task = table.Column<string>(type: "text", nullable: false),
                    model_name = table.Column<string>(type: "text", nullable: false),
                    model_provider = table.Column<string>(type: "text", nullable: false),
                    prompt_version = table.Column<string>(type: "text", nullable: true),
                    input_data = table.Column<string>(type: "jsonb", nullable: false),
                    output_data = table.Column<string>(type: "jsonb", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_model_run", x => x.id);
                    table.ForeignKey(
                        name: "FK_model_run_workflow_run_workflow_run_id",
                        column: x => x.workflow_run_id,
                        principalTable: "workflow_run",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "evaluation",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    model_run_id = table.Column<Guid>(type: "uuid", nullable: false),
                    evaluator_type = table.Column<string>(type: "text", nullable: false),
                    score = table.Column<decimal>(type: "numeric(3,2)", nullable: false),
                    pass = table.Column<bool>(type: "boolean", nullable: false),
                    feedback = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evaluation", x => x.id);
                    table.ForeignKey(
                        name: "FK_evaluation_model_run_model_run_id",
                        column: x => x.model_run_id,
                        principalTable: "model_run",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_evaluation_created_at",
                table: "evaluation",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_evaluation_evaluator_type",
                table: "evaluation",
                column: "evaluator_type");

            migrationBuilder.CreateIndex(
                name: "IX_evaluation_model_run_id",
                table: "evaluation",
                column: "model_run_id");

            migrationBuilder.CreateIndex(
                name: "IX_evaluation_pass",
                table: "evaluation",
                column: "pass");

            migrationBuilder.CreateIndex(
                name: "IX_evaluation_score",
                table: "evaluation",
                column: "score");

            migrationBuilder.CreateIndex(
                name: "IX_model_run_created_at",
                table: "model_run",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_model_run_model_name",
                table: "model_run",
                column: "model_name");

            migrationBuilder.CreateIndex(
                name: "IX_model_run_platform",
                table: "model_run",
                column: "platform");

            migrationBuilder.CreateIndex(
                name: "IX_model_run_task",
                table: "model_run",
                column: "task");

            migrationBuilder.CreateIndex(
                name: "IX_model_run_workflow_run_id",
                table: "model_run",
                column: "workflow_run_id");

            migrationBuilder.CreateIndex(
                name: "IX_workflow_run_external_execution_id_platform",
                table: "workflow_run",
                columns: new[] { "external_execution_id", "platform" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_workflow_run_platform",
                table: "workflow_run",
                column: "platform");

            migrationBuilder.CreateIndex(
                name: "IX_workflow_run_session_id",
                table: "workflow_run",
                column: "session_id");

            migrationBuilder.CreateIndex(
                name: "IX_workflow_run_started_at",
                table: "workflow_run",
                column: "started_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "evaluation");

            migrationBuilder.DropTable(
                name: "model_run");

            migrationBuilder.DropTable(
                name: "workflow_run");
        }
    }
}
