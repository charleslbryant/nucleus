using System.Runtime.Serialization;

namespace Nucleus.Domain.Enums;

/// <summary>
/// Defines the type  of feature.
/// </summary>
public enum FeatureType
{
    [EnumMember(Value = "unknown")]
    Unknown = 0,
    
    [EnumMember(Value = "observational")]
    Observational = 1,
    
    [EnumMember(Value = "interventional")]
    Interventional = 2,
    
    [EnumMember(Value = "methodological")]
    Methodological = 3
}