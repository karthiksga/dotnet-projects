using CQRS_MediatR_Demo.Models;
using CQRS_MediatR_Demo.Queries;
using CQRS_MediatR_Demo.Repositories;
using MediatR;

namespace CQRS_MediatR_Demo.Handlers
{
    public class GetStudentListHandler : IRequestHandler<GetStudentListQuery, List<StudentDetails>>
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentListHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<StudentDetails>> Handle(GetStudentListQuery query, CancellationToken cancellationToken)
        {
            return await _studentRepository.GetStudentListAsync();
        }
    }
}
