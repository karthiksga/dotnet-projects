using MediatR;

namespace CQRS_MediatR_Demo.Commands
{
    public class DeleteStudentCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
