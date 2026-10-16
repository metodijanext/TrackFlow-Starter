using Microsoft.EntityFrameworkCore;
using TrackFlow.Application.Interfaces;
using TrackFlow.Domain.Entities;
using TrackFlow.Infrastructure.Data;

namespace TrackFlow.Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly TrackFlowDbContext _db;

    public UserRepository(TrackFlowDbContext db)
    {
        _db = db;
    }

    public async Task<User?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken = default)
    {
        return await _db.Users.FindAsync(new object?[] { id }, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(
        string email, CancellationToken cancellationToken = default)
    {
        return await _db.Users.FirstOrDefaultAsync(
            u => u.Email == email, cancellationToken);
    }

    public async Task AddAsync(
        User user, CancellationToken cancellationToken = default)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
