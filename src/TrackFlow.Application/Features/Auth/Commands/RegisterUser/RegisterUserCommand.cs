using MediatR;

namespace TrackFlow.Application.Features.Auth.Commands.RegisterUser;

public record RegisterUserCommand(string Email,string Password,string FirstName,string LastName) : IRequest<RegisterUserResult>;
