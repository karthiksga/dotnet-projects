using CQRS_MediatR_Demo.Models;
using MediatR;

namespace CQRS_MediatR_Demo.Queries
{
    public class GetStudentListQuery : IRequest<List<StudentDetails>>
    {
    }
}
