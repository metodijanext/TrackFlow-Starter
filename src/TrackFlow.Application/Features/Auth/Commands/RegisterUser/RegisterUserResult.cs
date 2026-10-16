namespace TrackFlow.Application.Features.Auth.Commands.RegisterUser;

public record RegisterUserResult(
    bool IsSuccess,
    Guid? UserId = null,
    string? Email = null,
    string? FirstName = null,
    string? LastName = null,
    string? Role = null,
    string? AccessToken = null,
    string? RefreshToken = null,
    string? Error = null);
