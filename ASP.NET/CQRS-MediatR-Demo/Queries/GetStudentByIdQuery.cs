using CQRS_MediatR_Demo.Models;
using MediatR;

namespace CQRS_MediatR_Demo.Queries
{
    public class GetStudentByIdQuery : IRequest<StudentDetails>
    {
        public int Id { get; set; }
    }
}
