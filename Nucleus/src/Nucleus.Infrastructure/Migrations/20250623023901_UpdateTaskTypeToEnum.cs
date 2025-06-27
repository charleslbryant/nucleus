using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nucleus.Infrastructure.Migrations
{
    /// <summary>
    /// Migration to convert TaskType from string to integer enum values.
    /// 
    /// ⚠️  IMPORTANT: Maintenance Required ⚠️
    /// 
    /// This migration contains CASE statements that must be kept in sync with the C# TaskType enum.
    /// When modifying the TaskType enum in Nucleus.Domain.Enums.TaskType, you MUST update this migration.
    /// 
    /// Current mapping (must match C# enum exactly):
    /// - Unknown = 0 (default/fallback)
    /// - Summarize = 1
    /// - Classify = 2
    /// - Generate = 3
    /// - Translate = 4
    /// - Extract = 5
    /// - Analyze = 6
    /// - Draft = 7
    /// - QuestionAnswer = 8
    /// 
    /// ⚠️  WARNING: Adding, removing, or reordering enum values requires updating the CASE statements below!
    /// 
    /// Steps when modifying TaskType enum:
    /// 1. Update the C# enum in Nucleus.Domain.Enums.TaskType
    /// 2. Update the CASE statements in this migration's Up() method
    /// 3. Update the CASE statements in this migration's Down() method (if needed)
    /// 4. Test the migration thoroughly
    /// 5. Update this documentation
    /// </summary>
    public partial class UpdateTaskTypeToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Convert model_run.task column from text to integer
            // Maps string values to TaskType enum values
            migrationBuilder.Sql(@"
                ALTER TABLE model_run
                ALTER COLUMN task DROP DEFAULT;
                ALTER TABLE model_run
                ALTER COLUMN task TYPE integer USING (
                    CASE
                        WHEN task ~ '^[0-9]+$' THEN task::integer
                        WHEN lower(task) = 'summarize' THEN 1
                        WHEN lower(task) = 'classify' THEN 2
                        WHEN lower(task) = 'generate' THEN 3
                        WHEN lower(task) = 'translate' THEN 4
                        WHEN lower(task) = 'extract' THEN 5
                        WHEN lower(task) = 'analyze' THEN 6
                        WHEN lower(task) = 'draft' THEN 7
                        WHEN lower(task) = 'questionanswer' THEN 8
                        ELSE 0
                    END
                );
            ");

            // Convert evaluation_criteria.task_type column from text to integer
            // Maps string values to TaskType enum values
            migrationBuilder.Sql(@"
                ALTER TABLE evaluation_criteria
                ALTER COLUMN task_type DROP DEFAULT;
                ALTER TABLE evaluation_criteria
                ALTER COLUMN task_type TYPE integer USING (
                    CASE
                        WHEN task_type ~ '^[0-9]+$' THEN task_type::integer
                        WHEN lower(task_type) = 'summarize' THEN 1
                        WHEN lower(task_type) = 'classify' THEN 2
                        WHEN lower(task_type) = 'generate' THEN 3
                        WHEN lower(task_type) = 'translate' THEN 4
                        WHEN lower(task_type) = 'extract' THEN 5
                        WHEN lower(task_type) = 'analyze' THEN 6
                        WHEN lower(task_type) = 'draft' THEN 7
                        WHEN lower(task_type) = 'questionanswer' THEN 8
                        ELSE 0
                    END
                );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Convert back to string values
            // Note: This will lose the original string values and convert to generic names
            migrationBuilder.Sql(@"
                ALTER TABLE model_run
                ALTER COLUMN task TYPE text USING (
                    CASE
                        WHEN task = 0 THEN 'unknown'
                        WHEN task = 1 THEN 'summarize'
                        WHEN task = 2 THEN 'classify'
                        WHEN task = 3 THEN 'generate'
                        WHEN task = 4 THEN 'translate'
                        WHEN task = 5 THEN 'extract'
                        WHEN task = 6 THEN 'analyze'
                        WHEN task = 7 THEN 'draft'
                        WHEN task = 8 THEN 'questionanswer'
                        ELSE 'unknown'
                    END
                );
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE evaluation_criteria
                ALTER COLUMN task_type TYPE text USING (
                    CASE
                        WHEN task_type = 0 THEN 'unknown'
                        WHEN task_type = 1 THEN 'summarize'
                        WHEN task_type = 2 THEN 'classify'
                        WHEN task_type = 3 THEN 'generate'
                        WHEN task_type = 4 THEN 'translate'
                        WHEN task_type = 5 THEN 'extract'
                        WHEN task_type = 6 THEN 'analyze'
                        WHEN task_type = 7 THEN 'draft'
                        WHEN task_type = 8 THEN 'questionanswer'
                        ELSE 'unknown'
                    END
                );
            ");
        }
    }
}
