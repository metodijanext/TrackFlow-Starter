using TrackFlow.Domain.Entities;

namespace TrackFlow.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    (string AccessToken, string RefreshToken) GenerateTokenPair(User user);
}
