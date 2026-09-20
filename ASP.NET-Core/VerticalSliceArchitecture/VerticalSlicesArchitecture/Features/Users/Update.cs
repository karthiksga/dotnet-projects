using FastEndpoints;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using MediatR;
using VerticalSlicesArchitecture.Contracts.Requests;
using VerticalSlicesArchitecture.Contracts.Responses;
using VerticalSlicesArchitecture.Database;
using VerticalSlicesArchitecture.Entities;
using VerticalSlicesArchitecture.ErrorResponse;
using VerticalSlicesArchitecture.Mappers;

namespace VerticalSlicesArchitecture.Features.Users;

public static class Update
{
    public class Command : IRequest<Result<UserResponse>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int BirthYear { get; set; }
        public List<string> FavoriteColors { get; set; } = [];
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(command => command.FirstName)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(command => command.LastName)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(command => command.Email)
               .NotEmpty()
               .EmailAddress(EmailValidationMode.AspNetCoreCompatible);

            RuleFor(command => command.BirthYear)
                .NotEmpty()
                .LessThan(2024)
                .GreaterThan(1900);
        }
    }

    internal sealed class Handler(UserDbContext context, IValidator<Command> validator)
        : IRequestHandler<Command, Result<UserResponse>>
    {
        private readonly UserDbContext _context = context;
        private readonly IValidator<Command> _validator = validator;

        public async Task<Result<UserResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            ValidationResult? validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return Result.Failure<UserResponse>(new Error(
                    ErrorCodes.User.Update.Validation,
                    validationResult.ToString()));
            }

            User? existingUser = await _context.Users.FindAsync(request.Id);

            if (existingUser is null)
            {
                return Result.Failure<UserResponse>(new Error(
                    ErrorCodes.User.Get.NotFound,
                    "The user with the specified Id was not found"));
            }

            existingUser.FirstName = request.FirstName;
            existingUser.LastName = request.LastName;
            existingUser.Email = request.Email;
            existingUser.BirthYear = request.BirthYear;

            await _context.SaveChangesAsync(cancellationToken);

            return UserMapping.ToResponse(existingUser);
        }
    }
}

public class UpdateUserEndpoint(ISender sender) : Endpoint<UpdateUserRequest, Result<UserResponse>>
{
    private readonly ISender _sender = sender;

    public override void Configure()
    {
        Put("/api/users/update");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        //Write mappings
        Update.Command command = new()
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            BirthYear = request.BirthYear
        };

        Result<UserResponse>? result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            await SendResultAsync(Results.BadRequest(result.Error));
            return;
        }

        await SendOkAsync(result, cancellation: cancellationToken);
    }
}