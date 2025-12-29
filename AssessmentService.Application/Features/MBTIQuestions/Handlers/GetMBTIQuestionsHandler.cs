using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssessmentService.Application.Features.MBTIQuestions.Queries;
using AssessmentService.Application.Interfaces;
using FluentResults;
using JobSeeker.Shared.Contracts.MbtiTest;
using JobSeeker.Shared.Kernel.Abstractions;
using MassTransit.Internals;
using MassTransit.Testing;
using MediatR;

namespace AssessmentService.Application.Features.MBTIQuestions.Handlers
{
    public class GetMBTIQuestionsHandler : IRequestHandler<GetMBTIQuestionsQuery, Result<List<MBTIQuestionDTO>>>
    {
        private readonly IAssessmentServiceUnitOfWork _unitOfWork;

        public GetMBTIQuestionsHandler(IAssessmentServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<MBTIQuestionDTO>>> Handle(GetMBTIQuestionsQuery request, CancellationToken cancellationToken)
        {


            var questions = (await _unitOfWork.MBTIQuestionsRepository.GetAllAsync().Result.ToListAsync())
                           .Select(q => new MBTIQuestionDTO
                           {
                               Id = q.Id,
                               QuestionText = q.QuestionText,
                               Answers = new List<AnswerDtoMBTI>{
                                    new AnswerDtoMBTI
                                    {
                                        QuestionId = q.Id,
                                        Score = 1, //Yes 
                                    },
                                    new AnswerDtoMBTI
                                    {
                                        QuestionId = q.Id,
                                        Score = 2, //No
                                    }
                               }

                           })
                           .ToList();

            return Result.Ok(questions);
        }
    }
}
