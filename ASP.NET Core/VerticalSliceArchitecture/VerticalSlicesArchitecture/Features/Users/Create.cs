using FastEndpoints;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using MediatR;
using VerticalSlicesArchitecture.Contracts.Requests;
using VerticalSlicesArchitecture.Database;
using VerticalSlicesArchitecture.Entities;
using VerticalSlicesArchitecture.ErrorResponse;

namespace VerticalSlicesArchitecture.Features.Users;

public static class Create
{
    public class Command : IRequest<Result<Guid>>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int BirthYear { get; set; }
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
        : IRequestHandler<Command, Result<Guid>>
    {
        private readonly UserDbContext _context = context;
        private readonly IValidator<Command> _validator = validator;

        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            ValidationResult? validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return Result.Failure<Guid>(new Error(
                    ErrorCodes.User.Create.Validation,
                    validationResult.ToString()));
            }

            User user = new()
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                BirthYear = request.BirthYear
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}

public class CreateUserEndpoint(ISender sender) : Endpoint<CreateUserRequest, Result<Guid>>
{
    private readonly ISender _sender = sender;

    public override void Configure()
    {
        Post("/api/users/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        //Write mappings
        Create.Command command = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            BirthYear = request.BirthYear
        };

        Result<Guid>? result = await _sender.Send(command, cancellationToken);
  
        if(result.IsFailure)
        {
            await SendResultAsync(Results.BadRequest(result.Error));
            return;
        }

        await SendOkAsync(result, cancellation: cancellationToken);
    }
}