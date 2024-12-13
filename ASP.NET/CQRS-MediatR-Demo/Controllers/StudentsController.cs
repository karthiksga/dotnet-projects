using CQRS_MediatR_Demo.Commands;
using CQRS_MediatR_Demo.Models;
using CQRS_MediatR_Demo.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_MediatR_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public StudentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<List<StudentDetails>> GetStudentListAsync()
        {
            var studentDetails = await mediator.Send(new GetStudentListQuery());

            return studentDetails;
        }

        [HttpGet("studentId")]
        public async Task<StudentDetails> GetStudentByIdAsync(int studentId)
        {
            var studentDetails = await mediator.Send(new GetStudentByIdQuery() { Id = studentId });

            return studentDetails;
        }

        [HttpPost]
        public async Task<StudentDetails> AddStudentAsync(StudentDetails studentDetails)
        {
            var studentDetail = await mediator.Send(new CreateStudentCommand(
                studentDetails.Name,
                studentDetails.Email,
                studentDetails.Address,
                studentDetails.Age));
            return studentDetail;
        }

        [HttpPut]
        public async Task<int> UpdateStudentAsync(StudentDetails studentDetails)
        {
            var isStudentDetailUpdated = await mediator.Send(new UpdateStudentCommand(
               studentDetails.Id,
               studentDetails.Name,
               studentDetails.Email,
               studentDetails.Address,
               studentDetails.Age));
            return isStudentDetailUpdated;
        }

        [HttpDelete]
        public async Task<int> DeleteStudentAsync(int Id)
        {
            return await mediator.Send(new DeleteStudentCommand() { Id = Id });
        }
    }
}
