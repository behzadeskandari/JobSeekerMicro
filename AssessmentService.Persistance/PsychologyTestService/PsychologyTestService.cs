using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AssessmentService.Application.Interfaces;
using AssessmentService.Application.PsychologyTestInterfaces;
using AssessmentService.Domain.Entities;
using AssessmentService.Persistance.DbContexts;
using FluentResults;
using JobSeeker.Shared.Common.Services;
using JobSeeker.Shared.Contracts.Enums;
using JobSeeker.Shared.Contracts.IntegrationEvents;
using JobSeeker.Shared.Contracts.PsychologyTest;
using JobSeeker.Shared.Contracts.PsychologyTestQuestion;
using JobSeeker.Shared.Kernel.Abstractions;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace AssessmentService.Application.PsychologyTestService
{
    public class PsychologyTestService : IPsychologyTestService
    {
        private readonly IAssessmentServiceUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly AssessmentDbContext _context;
        private readonly IRequestClient<GetUserByIdRequestIntegrationEvent> _userRequestClient;

        public PsychologyTestService(
            IAssessmentServiceUnitOfWork unitOfWork, 
            ICurrentUserService currentUserService, 
            AssessmentDbContext context,
            IRequestClient<GetUserByIdRequestIntegrationEvent> userRequestClient)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _context = context;
            _userRequestClient = userRequestClient;
        }

        public async Task<List<PsychologyTestQuestionDto>> GetTestQuestionsAsync(int testId)
        {
            var answer = await _unitOfWork.AnswerOptionsRepository.GetQueryable().Where(x => x.PsychologyTestId == testId).ToListAsync();
            var questions = await _unitOfWork.PsychologyTestQuestionsRepository.GetQueryable()
                .Where(q => q.PsychologyTestId == testId)
                .Select(q => new PsychologyTestQuestionDto
                {
                    Id = q.Id,
                    QuestionText = q.QuestionText,
                    CorrectAnswer = q.CorrectAnswer,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    IsActive = true,
                    PsychologyTestId = testId,
                    QuestionType = q.QuestionType,
                    ScoringWeight = q.ScoringWeight,
                    AnswerOptions = answer.Select(x => new AnswerOptionDto
                    {
                        Id = x.Id,
                        Label = x.Label,
                        Value = x.Value,
                    }).ToList(),
                }).ToListAsync();
            return questions;
        }

        public async Task<Result> SubmitTestResponseAsync(PsychologyTestSubmissionDto submission)
        {
            var test = await _unitOfWork.PsychologyTestsRepository.GetQueryable()
                            .Include(t => t.Interpretation)
                            .FirstOrDefaultAsync(t => t.Id == submission.TestId);

            if (test == null)
                return Result.Fail("Test not found");

            // Get the current user ID
            var userId = _currentUserService.UserId;
            if (userId == null)
                return Result.Fail("Failed to find user");

            // Get user via event-driven request-response pattern
            var getUserResult = await GetUserByIdAsync(userId);
            if (getUserResult.IsFailed)
                return Result.Fail(getUserResult.Errors.FirstOrDefault()?.Message ?? "User not found");

            var user = getUserResult.Value;

            // Fetch all questions for the test to map responses correctly
            var questions = await _unitOfWork.PsychologyTestQuestionsRepository.GetQueryable()
                .Where(x => x.PsychologyTestId == test.Id)
                .ToDictionaryAsync(q => q.Id, q => q);

            // Validate that all submitted question IDs exist
            var invalidQuestionIds = submission.Answers
                .Select(a => a.QuestionId)
                .Where(qid => !questions.ContainsKey(qid))
                .ToList();
            if (invalidQuestionIds.Any())
                return Result.Fail($"Invalid question IDs: {string.Join(", ", invalidQuestionIds)}");

            // Check for existing test result for this user and test
            var existingResult = await _unitOfWork.PsychologyTestResultsRepository.GetQueryable()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.PsychologyTestId == submission.TestId);

            // Create or update test result
            var testResult = existingResult ?? new AssessmentService.Domain.Entities.PsychologyTestResult
            {
                UserId = userId,
                PsychologyTestId = submission.TestId,
                DateCreated = DateTime.Now,
                IsActive = true
            };

            testResult.Responses = submission.Answers.Select(x => new AssessmentService.Domain.Entities.PsychologyTestResponse
            {
                PsychologyTestQuestionId = x.QuestionId,
                Score = x.Score,
                SubmissionDate = DateTime.Now,
                DateCreated = DateTime.Now,
                PsychologyTestId = test.Id,
                PsychologyTest = test,
                PsychologyTestQuestion = questions[x.QuestionId],
                UserId = userId,
                //User = user,
                IsActive = true,
                Response = ""
            }).ToList();

            testResult.DateTaken = DateTime.Now;
            testResult.SubmissionDate = DateTime.Now;
            testResult.TotalScore = submission.Answers.Sum(x => x.Score);
            testResult.PsychologyTest = test;

            // Calculate result based on test type
            switch (test.Type)
            {
                case PsychologyTestType.MBTI:
                    testResult.ResultData = await CalculateMBTI(submission);
                    break;
                case PsychologyTestType.DISC:
                    testResult.ResultData = await CalculateDISC(submission);
                    break;
                case PsychologyTestType.BigFive:
                    testResult.ResultData = await CalculateBigFive(submission);
                    break;
                case PsychologyTestType.Holland:
                    testResult.ResultData = await CalculateHolland(submission);
                    break;
                case PsychologyTestType.EmotionalIntelligence:
                case PsychologyTestType.Cognitive:
                case PsychologyTestType.SJT:
                    testResult.ResultData = CalculateScoreBased(test, submission.Answers);
                    break;
                default:
                    return Result.Fail("Unsupported test type");
            }

            // Add or update test result
            if (existingResult == null)
            {
                await _unitOfWork.PsychologyTestResultsRepository.AddAsync(testResult);
            }
            else
            {
                await _unitOfWork.PsychologyTestResultsRepository.UpdateAsync(testResult);
            }

            // Create PsychologyTestResultAnswer
            var psychologyTestResultAnswer = new AssessmentService.Domain.Entities.PsychologyTestResultAnswer
            {
                UserId = userId,
                TestId = submission.TestId,
                Responses = submission.Answers.Select(x => new AssessmentService.Domain.Entities.PsychologyTestResponseAnswer
                {
                    PsychologyTestQuestionId = x.QuestionId,
                    Score = x.Score,
                    SubmissionDate = DateTime.Now,
                    DateCreated = DateTime.Now,
                    TestId = test.Id,
                    PsychologyTestQuestion = questions[x.QuestionId],
                    UserId = userId,
                    IsActive = true,
                    Response = "",
                }).ToList(),
                DateTaken = DateTime.Now,
                DateCreated = DateTime.Now,
                SubmissionDate = DateTime.Now,
                TotalScore = submission.Answers.Sum(x => x.Score),
                IsActive = true,
                ResultData = testResult.ResultData
            };

            await _unitOfWork.PsychologyTestResultAnswersRepository.AddAsync(psychologyTestResultAnswer);

            // Commit all changes in a single transaction
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
        //public async Task<Result> SubmitTestResponseAsync(PsychologyTestSubmissionDto submission)
        //{
        //    var test = await _context.psychologyTest.GetQueryable()
        //        .Include(t => t.Interpretation)
        //        .FirstOrDefaultAsync(t => t.Id == submission.TestId);
        //    var c = ClaimTypes.NameIdentifier;
        //    if (test == null)
        //        return Result.Fail("Test not found");

        //    var userId = _currentUserService.UserId;
        //    if (userId == null)
        //    {
        //        return Result.Fail("Failed To Find user ");
        //    }

        //    var user = await _context.UsersRepository.GetByIdAsync(userId);
        //    var questions = _context.psychologyTestQuestion.GetQueryable().FirstOrDefault(x => x.PsychologyTestId == test.Id);

        //    var testResult = new PsychologyTestResult
        //    {
        //        UserId = userId,
        //        PsychologyTestId = submission.TestId,
        //        Responses = submission.Answers.Select(x => new PsychologyTestResponse
        //        {
        //            PsychologyTestQuestionId = x.QuestionId,
        //            Score = x.Score,
        //            SubmissionDate = DateTime.Now,
        //            DateCreated = DateTime.Now,
        //            PsychologyTest = test,
        //            PsychologyTestId = test.Id,

        //            PsychologyTestQuestion = questions,
        //            User = user,
        //            UserId = userId,
        //            IsActive = true,
        //            Response = "",
        //        }).ToList(),
        //        DateTaken = DateTime.Now,
        //        DateCreated = DateTime.Now,
        //        SubmissionDate = DateTime.Now,
        //        TotalScore = submission.Answers.Sum(x => x.Score),
        //        IsActive = true,
        //        PsychologyTest = test,

        //    };
        //    var existingResponse = _context.psychologyTestResponse.GetQueryable().FirstOrDefault(x => x.UserId == userId);
        //    var existingResult = _context.psychologyTestResult.GetQueryable().FirstOrDefault(x => x.UserId == userId);


        //    switch (test.Type)
        //    {
        //        case PsychologyTestType.MBTI:
        //            testResult.ResultData = await CalculateMBTI(submission);
        //            break;
        //        case PsychologyTestType.DISC:
        //            testResult.ResultData = await CalculateDISC(submission);
        //            break;
        //        case PsychologyTestType.BigFive:
        //            testResult.ResultData = await CalculateBigFive(submission);
        //            break;
        //        case PsychologyTestType.Holland:
        //            testResult.ResultData = await CalculateHolland(submission);
        //            break;
        //        case PsychologyTestType.EmotionalIntelligence:
        //        case PsychologyTestType.Cognitive:
        //        case PsychologyTestType.SJT:
        //            testResult.ResultData = CalculateScoreBased(test, submission.Answers);
        //            break;
        //        default:
        //            return Result.Fail("Unsupported test type");
        //    }
        //    if (existingResult == null)
        //    {
        //        await _context.psychologyTestResult.AddAsync(testResult);
        //    }
        //    else
        //    {
        //        await _context.psychologyTestResult.UpdateAsync(testResult);
        //    }
        //    if (existingResponse == null)
        //    {
        //        await _context.psychologyTestResponse.AddRangeAsync(testResult.Responses);
        //    }
        //    else
        //    {
        //        await _context.psychologyTestResponse.UpdateRangeAsync(testResult.Responses);
        //    }
        //    await _context.CommitAsync();

        //    var psychologyTestResultAnswer = new PsychologyTestResultAnswer
        //    {
        //        UserId = userId,

        //        TestId = submission.TestId,
        //        Responses = submission.Answers.Select(x => new PsychologyTestResponseAnswer
        //        {
        //            PsychologyTestQuestionId = x.QuestionId,
        //            Score = x.Score,
        //            SubmissionDate = DateTime.Now,
        //            DateCreated = DateTime.Now,
        //            TestId = test.Id,
        //            PsychologyTestQuestion = questions,
        //            UserId = userId,
        //            IsActive = true,
        //            Response = "",
        //            TestResultId = test.Id
        //        }).ToList(),

        //        DateTaken = DateTime.Now,
        //        DateCreated = DateTime.Now,
        //        SubmissionDate = DateTime.Now,
        //        TotalScore = submission.Answers.Sum(x => x.Score),
        //        IsActive = true,
        //        ResultData = testResult.ResultData,
        //    };
        //    await _context.psychologyTestResponseAnswer.AddRangeAsync(psychologyTestResultAnswer.Responses);
        //    var lst = new List<PsychologyTestResultAnswer>();
        //    lst.Add(psychologyTestResultAnswer);
        //    await _context.psychologyTestResultAnswer.AddRangeAsync(lst);
        //    await _context.CommitAsync();

        //    return Result.Ok();
        //}

        
        // 🧠 For MBTI - simple demo logic, adjust to your real scoring keys
        private async Task<string> CalculateMBTI(PsychologyTestSubmissionDto submission)
        {
            var questions = await _unitOfWork.PsychologyTestQuestionsRepository.GetQueryable()
                .Where(q => q.PsychologyTestId == submission.TestId)
                .ToListAsync();

            var traits = new Dictionary<string, decimal>
        {
            {"E", 0}, {"I", 0},
            {"S", 0}, {"N", 0},
            {"T", 0}, {"F", 0},
            {"J", 0}, {"P", 0}
        };

            foreach (var answer in submission.Answers)
            {
                var question = questions.FirstOrDefault(q => q.Id == answer.QuestionId);
                if (question == null) continue;

                if (question.QuestionText.Contains("[E]")) traits["E"] += answer.Score;
                if (question.QuestionText.Contains("[I]")) traits["I"] += answer.Score;
                if (question.QuestionText.Contains("[S]")) traits["S"] += answer.Score;
                if (question.QuestionText.Contains("[N]")) traits["N"] += answer.Score;
                if (question.QuestionText.Contains("[T]")) traits["T"] += answer.Score;
                if (question.QuestionText.Contains("[F]")) traits["F"] += answer.Score;
                if (question.QuestionText.Contains("[J]")) traits["J"] += answer.Score;
                if (question.QuestionText.Contains("[P]")) traits["P"] += answer.Score;
            }

            return $"{(traits["E"] >= traits["I"] ? "E" : "I")}" +
                   $"{(traits["S"] >= traits["N"] ? "S" : "N")}" +
                   $"{(traits["T"] >= traits["F"] ? "T" : "F")}" +
                   $"{(traits["J"] >= traits["P"] ? "J" : "P")}";
        }

        private async Task<string> CalculateDISC(PsychologyTestSubmissionDto submission)
        {
            var questions = await _unitOfWork.PsychologyTestQuestionsRepository.GetQueryable()
                .Where(q => q.PsychologyTestId == submission.TestId)
                .ToListAsync();

            var traits = new Dictionary<string, decimal> { { "D", 0 }, { "I", 0 }, { "S", 0 }, { "C", 0 } };

            foreach (var answer in submission.Answers)
            {
                var question = questions.FirstOrDefault(q => q.Id == answer.QuestionId);
                if (question == null) continue;

                if (question.QuestionText.Contains("[D]")) traits["D"] += answer.Score;
                if (question.QuestionText.Contains("[I]")) traits["I"] += answer.Score;
                if (question.QuestionText.Contains("[S]")) traits["S"] += answer.Score;
                if (question.QuestionText.Contains("[C]")) traits["C"] += answer.Score;
            }

            return traits.OrderByDescending(x => x.Value).First().Key;
        }

        private async Task<string> CalculateBigFive(PsychologyTestSubmissionDto submission)
        {
            var questions = await _unitOfWork.PsychologyTestQuestionsRepository.GetQueryable()
                .Where(q => q.PsychologyTestId == submission.TestId)
                .ToListAsync();

            var traits = new Dictionary<string, decimal>
        {
            { "O", 0 }, { "C", 0 }, { "E", 0 }, { "A", 0 }, { "N", 0 }
        };

            foreach (var answer in submission.Answers)
            {
                var question = questions.FirstOrDefault(q => q.Id == answer.QuestionId);
                if (question == null) continue;

                if (question.QuestionText.Contains("[O]")) traits["O"] += answer.Score;
                if (question.QuestionText.Contains("[C]")) traits["C"] += answer.Score;
                if (question.QuestionText.Contains("[E]")) traits["E"] += answer.Score;
                if (question.QuestionText.Contains("[A]")) traits["A"] += answer.Score;
                if (question.QuestionText.Contains("[N]")) traits["N"] += answer.Score;
            }

            return string.Join(",", traits.Select(t => $"{t.Key}:{t.Value}"));
        }

        private async Task<string> CalculateHolland(PsychologyTestSubmissionDto submission)
        {
            var questions = await _unitOfWork.PsychologyTestQuestionsRepository.GetQueryable()
                .Where(q => q.PsychologyTestId == submission.TestId)
                .ToListAsync();

            var types = new Dictionary<string, decimal>
        {
            { "R", 0 }, { "I", 0 }, { "A", 0 }, { "S", 0 }, { "E", 0 }, { "C", 0 }
        };

            foreach (var answer in submission.Answers)
            {
                var question = questions.FirstOrDefault(q => q.Id == answer.QuestionId);
                if (question == null) continue;

                if (question.QuestionText.Contains("[R]")) types["R"] += answer.Score;
                if (question.QuestionText.Contains("[I]")) types["I"] += answer.Score;
                if (question.QuestionText.Contains("[A]")) types["A"] += answer.Score;
                if (question.QuestionText.Contains("[S]")) types["S"] += answer.Score;
                if (question.QuestionText.Contains("[E]")) types["E"] += answer.Score;
                if (question.QuestionText.Contains("[C]")) types["C"] += answer.Score;
            }

            return string.Join(",", types.OrderByDescending(t => t.Value).Select(t => t.Key));
        }

        private string CalculateScoreBased(PsychologyTest test, List<QuestionAnswerDto> answers)
        {
            var totalScore = answers.Sum(x => x.Score);
            var interpretation = test.Interpretation.FirstOrDefault(i => totalScore >= i.MinScore && totalScore <= i.MaxScore);
            return interpretation?.Interpretation ?? "نتیجه‌ای یافت نشد.";
        }
        //public async Task<List<PsychologyTestQuestionDto>> GetTestQuestionsAsync(int testId)
        //{
        //    var questions = await _context.psychologyTestQuestion.GetQueryable()
        //        .Where(q => q.PsychologyTestId == testId)
        //        .Select(q => new PsychologyTestQuestionDto
        //        {
        //            Id = q.Id,
        //            QuestionText = q.QuestionText,
        //            CorrectAnswer = q.CorrectAnswer,
        //            DateCreated = DateTime.Now,
        //            DateModified = DateTime.Now,    
        //            IsActive = true,
        //            PsychologyTestId = testId,
        //            QuestionType = q.QuestionType,  
        //            ScoringWeight = q.ScoringWeight,
        //        }).ToListAsync();

        //    return questions;
        //}

        //public async Task<Result> SubmitTestResponseAsync(PsychologyTestSubmissionDto submission)
        //{
        //    var test = await _context.psychologyTest.GetQueryable()
        //        .Include(t => t.Interpretation)
        //        .FirstOrDefaultAsync(t => t.Id == submission.TestId);

        //    if (test == null)
        //        return Result.Fail("Test not found");

        //    var totalScore = submission.Answers.Sum(x => x.Score);

        //    var interpretation = test.Interpretation
        //        .FirstOrDefault(i => totalScore >= i.MinScore && totalScore <= i.MaxScore)?.Interpretation;
        //    var userId = _currentUserService.UserId;

        //    var testResult = new PsychologyTestResult
        //    {
        //        UserId = userId,
        //        PsychologyTestId = submission.TestId,
        //        TotalScore = totalScore,
        //        Responses = submission.Answers.Select(x => new PsychologyTestResponse
        //        {
        //            PsychologyTestQuestionId = x.QuestionId,
        //            Score = x.Score,
        //        }).ToList()
        //    };
        //    testResult.Interpretation.First().Interpretation = interpretation;
        //    await _context.psychologyTestResult.AddAsync(testResult);
        //    await _context.CommitAsync(cancellationToken: CancellationToken.None);
        //    return Result.Ok();
        //}


        private async Task<Result<UserDto>> GetUserByIdAsync(string userId)
        {
            try
            {
                var requestID = Guid.NewGuid();
                var outBoxMessage = new OutboxMessage()
                {
                    Content = JsonSerializer.Serialize(userId),
                    Id = requestID,
                    OccurredOn = DateTime.Now,
                    Published = false,
                    Type = "UserId",
                };

                var request = new GetUserByIdRequestIntegrationEvent(userId, requestID);

                var response = await _userRequestClient.GetResponse<GetUserByIdResponseIntegrationEvent>(
                    request,
                    timeout: TimeSpan.FromSeconds(10)
                );

                if (response.Message.IsSuccess && response.Message.User != null)
                {

                    return Result.Ok(response.Message.User);
                    outBoxMessage.Published = true;
                    await _context.OutboxMessage.AddAsync(outBoxMessage);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    outBoxMessage.Published = false;
                    await _context.OutboxMessage.AddAsync(outBoxMessage);
                    await _context.SaveChangesAsync();
                    return Result.Fail(response.Message.ErrorMessage ?? "User not found");
                }
            }
            catch (RequestTimeoutException)
            {
                return Result.Fail("Request timeout - IdentityService did not respond");
            }
            catch (Exception ex)
            {
                return Result.Fail($"Error getting user: {ex.Message}");
            }
        }

    }

}
