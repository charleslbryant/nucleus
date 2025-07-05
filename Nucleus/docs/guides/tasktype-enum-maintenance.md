# TaskType Enum Maintenance Guide

## Overview

The `TaskType` enum in `Nucleus.Domain.Enums.TaskType` is tightly coupled with database migrations. This document explains the maintenance requirements and provides a checklist for future changes.

## Current Mapping

| Enum Value         | C# Value | Database Value | String Mapping |
|--------------------|----------|----------------|----------------|
| Unknown            | 0        | 0              | 'unknown'      |
| Summarize          | 1        | 1              | 'summarize'    |
| Classify           | 2        | 2              | 'classify'     |
| Generate           | 3        | 3              | 'generate'     |
| Translate          | 4        | 4              | 'translate'    |
| Extract            | 5        | 5              | 'extract'      |
| Analyze            | 6        | 6              | 'analyze'      |
| Draft              | 7        | 7              | 'draft'        |
| QuestionAnswer     | 8        | 8              | 'questionanswer' |

## ⚠️ Critical Maintenance Requirements

### When Modifying the TaskType Enum

**You MUST update the following files:**

1. **`Nucleus.Domain.Enums.TaskType.cs`** - The enum definition
2. **`Nucleus.Infrastructure/Migrations/20250623023901_UpdateTaskTypeToEnum.cs`** - The migration CASE statements
3. **This documentation file** - Keep the mapping table current

### Checklist for Enum Changes

- [ ] Update the C# enum in `TaskType.cs`
- [ ] Update the CASE statements in the migration's `Up()` method
- [ ] Update the CASE statements in the migration's `Down()` method
- [ ] Update the documentation in the enum file
- [ ] Update this mapping table
- [ ] Test the migration thoroughly
- [ ] Verify all existing data converts correctly

## Migration Details

### Files to Update

1. **Enum Definition**: `src/Nucleus.Domain/Enums/TaskType.cs`
2. **Migration**: `src/Nucleus.Infrastructure/Migrations/20250623023901_UpdateTaskTypeToEnum.cs`
3. **Documentation**: `docs/tasktype-enum-maintenance.md`

### Migration Logic

The migration uses PostgreSQL CASE statements to convert string values to integer enum values:

```sql
CASE
    WHEN task ~ '^[0-9]+$' THEN task::integer
    WHEN lower(task) = 'summarize' THEN 1
    WHEN lower(task) = 'classify' THEN 2
    -- ... etc
    ELSE 0
END
```

## Best Practices

### Adding New Task Types

1. **Add to enum** with the next available integer value
2. **Update migration** CASE statements in both Up() and Down() methods
3. **Update documentation** in all relevant files
4. **Test thoroughly** with existing data

### Removing Task Types

1. **Mark as deprecated** in the enum (don't remove immediately)
2. **Update migration** to handle deprecated values
3. **Plan data migration** for existing records
4. **Remove after** all data is migrated

### Reordering Task Types

**⚠️ Avoid this if possible!** Reordering changes the integer values and requires:
1. **Data migration** to preserve existing records
2. **Update all CASE statements** in migrations
3. **Comprehensive testing** of existing data

## Future Considerations

### Moving to Domain Entity

Consider migrating from enum to a `TaskType` domain entity when:
- Dynamic task types are needed
- Task types need metadata (description, validation rules, etc.)
- Admin interface for managing task types is required

### Benefits of Domain Entity Approach
- No maintenance coupling between C# and SQL
- Dynamic task type management
- Rich metadata support
- Better extensibility

## Troubleshooting

### Common Issues

1. **Migration fails**: Check that CASE statements match enum values exactly
2. **Data loss**: Verify Down() migration preserves data correctly
3. **Type mismatches**: Ensure all layers use the same enum values

### Testing

Always test migrations with:
- [ ] Empty database
- [ ] Database with existing string data
- [ ] Database with existing integer data
- [ ] Rollback scenarios (Down() migration)

## Related Files

- `src/Nucleus.Domain/Enums/TaskType.cs`
- `src/Nucleus.Infrastructure/Migrations/20250623023901_UpdateTaskTypeToEnum.cs`
- `src/Nucleus.Domain/Entities/ModelRun.cs`
- `src/Nucleus.Domain/Entities/EvaluationCriteria.cs`
- `src/Nucleus.Application/Features/EvaluateModelRun/EvaluateModelRunCommand.cs`
- `src/Nucleus.Presentation/Nucleus.Api/DTOs/EvaluateModelRunRequest.cs` 