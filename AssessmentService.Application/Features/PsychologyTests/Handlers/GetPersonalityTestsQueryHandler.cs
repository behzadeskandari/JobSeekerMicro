using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssessmentService.Application.Interfaces;
using AutoMapper;
using FluentResults;
using JobSeeker.Application.Feature.PsychologyTests.Queries;
using JobSeeker.Shared.Contracts.PersonalityTest;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssessmentService.Application.Features.PsychologyTests.Handlers
{
    public class GetPersonalityTestsQueryHandler : IRequestHandler<GetPersonalityTestsQuery, Result<IEnumerable<PersonalityTestDto>>>
    {
        private readonly IAssessmentServiceUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPersonalityTestsQueryHandler(IAssessmentServiceUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<PersonalityTestDto>>> Handle(GetPersonalityTestsQuery request, CancellationToken cancellationToken)
        {
            var tests = await _unitOfWork.PsychologyTestResultsRepository.GetQueryable()
                .Where(pt => pt.IsActive == true)
                .ToListAsync(cancellationToken);

            var testDtos = _mapper.Map<IEnumerable<PersonalityTestDto>>(tests);
            return Result.Ok(testDtos);
        }
    }
}
