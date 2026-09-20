using TrackFlow.Domain.Common;

namespace TrackFlow.Domain.Entities;

/// <summary>
/// Project entity for managing time tracking projects.
/// </summary>
public class Project : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid ManagerId { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public User? Manager { get; set; }
    public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
    public ICollection<ProjectUser> TeamMembers { get; set; } = new List<ProjectUser>();
}
