using VerticalSlicesArchitecture.Contracts.Responses;
using VerticalSlicesArchitecture.Entities;

namespace VerticalSlicesArchitecture.Mappers;

public class UserMapping
{
    public static UserResponse ToResponse(User user)
    {
        return new UserResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }
}