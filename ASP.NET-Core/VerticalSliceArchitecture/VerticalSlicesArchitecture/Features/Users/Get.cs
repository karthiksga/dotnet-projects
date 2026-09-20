using FastEndpoints;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VerticalSlicesArchitecture.Contracts.Requests;
using VerticalSlicesArchitecture.Contracts.Responses;
using VerticalSlicesArchitecture.Database;
using VerticalSlicesArchitecture.ErrorResponse;

namespace VerticalSlicesArchitecture.Features.Users;

public static class Get
{
    public class Query : IRequest<Result<UserResponse>>
    {
        public Guid Id { get; set; }
    }

    internal sealed class Handler(UserDbContext context) : IRequestHandler<Query, Result<UserResponse>>
    {
        private readonly UserDbContext _context = context;

        public async Task<Result<UserResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            UserResponse? user = await _context
                .Users
                .AsNoTracking()
                .Where(user => user.Id == request.Id)
                .Select(user => new UserResponse
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
            {
                return Result.Failure<UserResponse>(new Error(
                    ErrorCodes.User.Get.NotFound,
                    "The user with the specified Id was not found"));
            }

            return user;
        }
    }
}

public class GetUserEndpoint(ISender sender) : Endpoint<GetUserRequest, Result<UserResponse>>
{
    private readonly ISender _sender = sender;

    public override void Configure()
    {
        Get("/api/user/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetUserRequest req, CancellationToken cancellationToken)
    {
        Get.Query query = new() { Id = req.Id };

        Result<UserResponse> result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            await SendResultAsync(Results.BadRequest(result.Error));
            return;
        }

        await SendOkAsync(result, cancellation: cancellationToken);
    }
}