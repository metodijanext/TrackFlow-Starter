using TrackFlow.Domain.Common;

namespace TrackFlow.Domain.Entities;

/// <summary>
/// TimeEntry entity representing logged work time on a project.
/// NOTE: This entity is defined but CRUD operations are left for Week 4 student implementation.
/// </summary>
public class TimeEntry : BaseEntity
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    
    public DateTime EntryDate { get; set; }
    public decimal Hours { get; set; }
    public string? Description { get; set; }
    public string? TaskDescription { get; set; }

    // Navigation properties
    public Project? Project { get; set; }
    public User? User { get; set; }

    public TimeEntry() { }

    public TimeEntry(Guid projectId, Guid userId, DateTime entryDate, decimal hours)
    {
        ProjectId = projectId;
        UserId = userId;
        EntryDate = entryDate;
        Hours = hours;
    }
}
