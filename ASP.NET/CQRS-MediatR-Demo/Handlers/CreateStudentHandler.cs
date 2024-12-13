using CQRS_MediatR_Demo.Commands;
using CQRS_MediatR_Demo.Models;
using CQRS_MediatR_Demo.Repositories;
using MediatR;

namespace CQRS_MediatR_Demo.Handlers
{
    public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, StudentDetails>
    {
        private readonly IStudentRepository _studentRepository;

        public CreateStudentHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<StudentDetails> Handle(CreateStudentCommand command, CancellationToken cancellationToken)
        {
            var studentDetails = new StudentDetails()
            {
                Name = command.Name,
                Email = command.Email,
                Address = command.Address,
                Age = command.Age
            };

            return await _studentRepository.AddStudentAsync(studentDetails);
        }
    }
}
