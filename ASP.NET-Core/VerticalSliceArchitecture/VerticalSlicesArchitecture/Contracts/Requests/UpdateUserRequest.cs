namespace VerticalSlicesArchitecture.Contracts.Requests;

public class UpdateUserRequest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int BirthYear { get; set; }
}