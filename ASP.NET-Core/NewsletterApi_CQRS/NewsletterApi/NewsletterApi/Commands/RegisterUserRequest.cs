using MediatR;

namespace NewsletterApi.Commands;

public record RegisterUserRequest(string Username, string Email, string Password) : IRequest<string>;