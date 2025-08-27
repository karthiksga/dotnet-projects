using FastEndpoints;
using MediatR;
using VerticalSlicesArchitecture.Contracts.Requests;
using VerticalSlicesArchitecture.Database;
using VerticalSlicesArchitecture.Entities;
using VerticalSlicesArchitecture.ErrorResponse;

namespace VerticalSlicesArchitecture.Features.Users;

public static class Delete
{
    public class Command : IRequest<Result>
    {
        public Guid Id { get; set; }
    }

    internal sealed class Handler(UserDbContext context) : IRequestHandler<Command, Result>
    {
        private readonly UserDbContext _context = context;

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            User? user = await _context.Users.FindAsync([request.Id], cancellationToken: cancellationToken);
               
            if (user is null)
            {
                return Result.Failure(new Error(
                    ErrorCodes.User.Get.NotFound,
                    "The user with the specified Id was not found"));
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

public class DeleteUserEndpoint(ISender sender) : Endpoint<GetUserRequest, Result>
{
    private readonly ISender _sender = sender;

    public override void Configure()
    {
        Delete("/api/user/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetUserRequest request, CancellationToken cancellationToken)
    {
        Delete.Command command = new() { Id = request.Id };

        Result? result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            await SendResultAsync(Results.BadRequest(result.Error));
            return;
        }

        await SendOkAsync(result, cancellation: cancellationToken);
    }
}