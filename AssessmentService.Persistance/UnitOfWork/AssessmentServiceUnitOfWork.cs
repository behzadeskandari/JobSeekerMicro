using AssessmentService.Application.Interfaces;
using AssessmentService.Persistance.DbContexts;
using AssessmentService.Persistance.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentService.Persistance.UnitOfWork
{
    internal class AssessmentServiceUnitOfWork : IAssessmentServiceUnitOfWork
    {
        private readonly AssessmentDbContext _context;
        public IAnswerOptionsRepository _answerOptionsRepository;
        public IAnswerOptionsRepository AnswerOptionsRepository => _answerOptionsRepository  ??= new AnswerOptionsRepository(_context);



        public IMBTIQuestionsRepository _mBTIQuestionsRepository;
        public IMBTIQuestionsRepository MBTIQuestionsRepository => _mBTIQuestionsRepository ??= new MBTIQuestionsRepository(_context);



        public IMBTIResultAnswersRepository _mBTIResultAnswersRepository ;
        public IMBTIResultAnswersRepository MBTIResultAnswersRepository => _mBTIResultAnswersRepository ??= new MBTIResultAnswersRepository(_context);



        public IMBTIResultsRepository _mBTIResultsRepository;
        public IMBTIResultsRepository MBTIResultsRepository => _mBTIResultsRepository ??= new MBTIResultsRepository(_context);




        public IPersonalityTestItemsRepository _personalityTestItemsRepository ;
        public IPersonalityTestItemsRepository PersonalityTestItemsRepository => _personalityTestItemsRepository ??= new PersonalityTestItemsRepository(_context);



        public IPersonalityTestResponsesRepository _personalityTestResponsesRepository;
        public IPersonalityTestResponsesRepository PersonalityTestResponsesRepository => _personalityTestResponsesRepository ??= new PersonalityTestResponsesRepository(_context);


        public IPersonalityTestResultsRepository _personalityTestResultsRepository;
        public IPersonalityTestResultsRepository PersonalityTestResultsRepository => _personalityTestResultsRepository ?? new PersonalityTestResultsRepository(_context);




        public IPersonalityTraitsRepository _personalityTraitsRepository;
        public IPersonalityTraitsRepository PersonalityTraitsRepository => _personalityTraitsRepository ?? new PersonalityTraitsRepository(_context);


        public IPsychologyTestInterpretationRepository _psychologyTestInterpretationRepository;
        public IPsychologyTestInterpretationRepository PsychologyTestInterpretationRepository => _psychologyTestInterpretationRepository ??= new PsychologyTestInterpretationRepository(_context);



        public IPsychologyTestQuestionsRepository _psychologyTestQuestionsRepository;
        public IPsychologyTestQuestionsRepository PsychologyTestQuestionsRepository => _psychologyTestQuestionsRepository ??= new PsychologyTestQuestionsRepository(_context);


        public IPsychologyTestResponseAnswersRepository _psychologyTestResponseAnswersRepository;
        public IPsychologyTestResponseAnswersRepository PsychologyTestResponseAnswersRepository => _psychologyTestResponseAnswersRepository ??= new PsychologyTestResponseAnswersRepository(_context);


        public IPsychologyTestResponsesRepository _psychologyTestResponsesRepository;
        public IPsychologyTestResponsesRepository PsychologyTestResponsesRepository => _psychologyTestResponsesRepository ??= new PsychologyTestResponsesRepository(_context);


        public IPsychologyTestResultAnswersRepository _psychologyTestResultAnswersRepository;
        public IPsychologyTestResultAnswersRepository PsychologyTestResultAnswersRepository => _psychologyTestResultAnswersRepository ??= new PsychologyTestResultAnswersRepository(_context);


        public IPsychologyTestResultsRepository _psychologyTestResultsRepository;
        public IPsychologyTestResultsRepository PsychologyTestResultsRepository => _psychologyTestResultsRepository ??= new PsychologyTestResultsRepository(_context);

        
        public IPsychologyTestsRepository _psychologyTestsRepository;
        public IPsychologyTestsRepository PsychologyTestsRepository => _psychologyTestsRepository ??= new PsychologyTestsRepository(_context);

        public ILogRepository _logs;
        public ILogRepository Logs => _logs ??= new LogRepository(_context);

        public IExceptionLogRepository _exceptionLogs;
        public IExceptionLogRepository ExceptionLogs => _exceptionLogs ??= new ExceptionLogRepository(_context);

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }


        public void Dispose() => _context.Dispose();

    }
}
