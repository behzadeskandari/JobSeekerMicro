using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobSeeker.Shared.Contracts.MbtiTest;
using MediatR;

namespace AssessmentService.Application.Features.MBTIQuestions.Queries
{
    public class GetMBTIQuestionsQuery : IRequest<Result<List<MBTIQuestionDTO>>> { }
}
