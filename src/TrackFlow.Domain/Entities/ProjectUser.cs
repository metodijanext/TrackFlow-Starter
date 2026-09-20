using TrackFlow.Domain.Common;

namespace TrackFlow.Domain.Entities;

/// <summary>
/// Junction entity for many-to-many relationship between Project and User.
/// </summary>
public class ProjectUser : BaseEntity
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Project? Project { get; set; }
    public User? User { get; set; }
}
