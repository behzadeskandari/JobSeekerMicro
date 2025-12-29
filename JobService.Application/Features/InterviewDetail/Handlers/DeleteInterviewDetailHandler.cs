using System.Threading.Tasks;
using JobService.Application.Features.InterviewDetail.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.InterviewDetail.Handlers
{
    public class DeleteInterviewDetailHandler : IRequestHandler<DeleteInterviewDetailCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public DeleteInterviewDetailHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteInterviewDetailCommand request, CancellationToken cancellationToken)
        {
            var interview = await _repository.InterviewDetail.GetByIdAsync(request.Id);
            if (interview == null)
            {
                return Result.Fail("InterviewDetail not found");
            }

            await _repository.InterviewDetail.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

