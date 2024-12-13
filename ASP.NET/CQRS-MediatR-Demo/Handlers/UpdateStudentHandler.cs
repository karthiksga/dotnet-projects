using CQRS_MediatR_Demo.Commands;
using CQRS_MediatR_Demo.Repositories;
using MediatR;

namespace CQRS_MediatR_Demo.Handlers
{
    public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, int>
    {
        private readonly IStudentRepository _studentRepository;

        public UpdateStudentHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<int> Handle(UpdateStudentCommand command, CancellationToken cancellationToken)
        {
            var studentDetails = await _studentRepository.GetStudentByIdAsync(command.Id);
            if (studentDetails == null)
                return default;

            studentDetails.Name = command.Name;
            studentDetails.Email = command.Email;
            studentDetails.Address = command.Address;
            studentDetails.Age = command.Age;

            return await _studentRepository.UpdateStudentAsync(studentDetails);
        }
    }
}
