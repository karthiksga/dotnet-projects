using MediatR;

namespace CQRS_MediatR_Demo.Commands
{
    public class UpdateStudentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }

        public UpdateStudentCommand(int id, string studentName, string studentEmail, string studentAddress, int studentAge)
        {
            Id = id;
            Name = studentName;
            Email = studentEmail;
            Address = studentAddress;
            Age = studentAge;
        }
    }
}
