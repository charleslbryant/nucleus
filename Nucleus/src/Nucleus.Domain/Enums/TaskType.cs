namespace Nucleus.Domain.Enums;

/// <summary>
/// Represents the type of AI task being performed.
/// 
/// ⚠️  IMPORTANT: Database Migration Dependency ⚠️
/// 
/// This enum is tightly coupled with database migrations. When you modify this enum:
/// 1. Update the corresponding migration CASE statements in:
///    - 20250623023901_UpdateTaskTypeToEnum.cs (Up method)
/// 2. Ensure the integer values match between C# and SQL
/// 3. Test the migration thoroughly
/// 
/// Current mapping:
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
/// ⚠️  WARNING: Adding, removing, or reordering enum values requires migration updates!
/// </summary>
public enum TaskType
{
    /// <summary>
    /// Unknown or unspecified task type.
    /// Database value: 0
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// Summarize content or text.
    /// Database value: 1
    /// </summary>
    Summarize = 1,
    
    /// <summary>
    /// Classify or categorize content.
    /// Database value: 2
    /// </summary>
    Classify = 2,
    
    /// <summary>
    /// Generate new content or text.
    /// Database value: 3
    /// </summary>
    Generate = 3,
    
    /// <summary>
    /// Translate content between languages.
    /// Database value: 4
    /// </summary>
    Translate = 4,
    
    /// <summary>
    /// Extract specific information from content.
    /// Database value: 5
    /// </summary>
    Extract = 5,
    
    /// <summary>
    /// Analyze content for insights or patterns.
    /// Database value: 6
    /// </summary>
    Analyze = 6,
    
    /// <summary>
    /// Draft or compose new content.
    /// Database value: 7
    /// </summary>
    Draft = 7,
    
    /// <summary>
    /// Answer questions based on content.
    /// Database value: 8
    /// </summary>
    QuestionAnswer = 8
} 