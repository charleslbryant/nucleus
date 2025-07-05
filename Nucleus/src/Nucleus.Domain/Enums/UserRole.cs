namespace Nucleus.Domain.Enums;

/// <summary>
/// Represents the role of a user in the system.
/// 
/// ⚠️  IMPORTANT: Database Migration Dependency ⚠️
/// 
/// This enum is tightly coupled with database migrations. When you modify this enum:
/// 1. Update the corresponding migration CASE statements
/// 2. Ensure the integer values match between C# and SQL
/// 3. Test the migration thoroughly
/// 
/// Current mapping:
/// - Guest = 0 (default/fallback)
/// - Reviewer = 1
/// - Admin = 2
/// 
/// ⚠️  WARNING: Adding, removing, or reordering enum values requires migration updates!
/// </summary>
public enum UserRole
{
    /// <summary>
    /// Guest user with read-only access to public evaluations.
    /// Database value: 0
    /// </summary>
    Guest = 0,
    
    /// <summary>
    /// Reviewer user with access to view and provide feedback on evaluations.
    /// Database value: 1
    /// </summary>
    Reviewer = 1,
    
    /// <summary>
    /// Administrator with full access to all system features and user management.
    /// Database value: 2
    /// </summary>
    Admin = 2
} 