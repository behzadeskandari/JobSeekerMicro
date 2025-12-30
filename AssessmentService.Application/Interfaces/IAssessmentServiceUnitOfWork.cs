using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentService.Application.Interfaces
{
    public interface IAssessmentServiceUnitOfWork : IDisposable
    {
        public IAnswerOptionsRepository AnswerOptionsRepository { get; }
        public IMBTIQuestionsRepository MBTIQuestionsRepository { get; }
        public IMBTIResultAnswersRepository MBTIResultAnswersRepository { get; }
        public IMBTIResultsRepository MBTIResultsRepository { get; }
        public IPersonalityTestItemsRepository PersonalityTestItemsRepository { get; }
        public IPersonalityTestResponsesRepository PersonalityTestResponsesRepository { get; }
        public IPersonalityTestResultsRepository PersonalityTestResultsRepository { get; }
        public IPersonalityTraitsRepository PersonalityTraitsRepository { get; }
        public IPsychologyTestInterpretationRepository PsychologyTestInterpretationRepository { get; }
        public IPsychologyTestQuestionsRepository PsychologyTestQuestionsRepository { get; }
        public IPsychologyTestResponseAnswersRepository PsychologyTestResponseAnswersRepository { get; }
        public IPsychologyTestResponsesRepository PsychologyTestResponsesRepository { get; }
        public IPsychologyTestResultAnswersRepository PsychologyTestResultAnswersRepository { get; }
        public IPsychologyTestResultsRepository PsychologyTestResultsRepository { get; }
        public IPsychologyTestsRepository PsychologyTestsRepository { get; }
        ILogRepository Logs { get; }
        IExceptionLogRepository ExceptionLogs { get; }
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
