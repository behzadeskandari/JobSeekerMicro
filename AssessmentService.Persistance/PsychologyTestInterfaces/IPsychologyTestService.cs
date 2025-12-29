using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobSeeker.Shared.Contracts.PsychologyTest;
using JobSeeker.Shared.Contracts.PsychologyTestQuestion;

namespace AssessmentService.Application.PsychologyTestInterfaces
{
    public interface IPsychologyTestService
    {
        Task<List<PsychologyTestQuestionDto>> GetTestQuestionsAsync(int testId);
        Task<Result> SubmitTestResponseAsync(PsychologyTestSubmissionDto submission);
    }
}
