using MediatR;
using TrackFlow.Application.Dtos.Users;

namespace TrackFlow.Application.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDto?>;
