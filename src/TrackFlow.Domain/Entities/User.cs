using TrackFlow.Domain.Common;

namespace TrackFlow.Domain.Entities;

/// <summary>
/// User entity representing a system user with authentication and role-based access.
/// </summary>
public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Employee;
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public ICollection<Project> ManagedProjects { get; set; } = new List<Project>();
    public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
    public ICollection<ProjectUser> ProjectAssignments { get; set; } = new List<ProjectUser>();

    public string GetFullName() => $"{FirstName} {LastName}".Trim();
}
