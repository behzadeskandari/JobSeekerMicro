using FluentResults;
using JobSeeker.Shared.Contracts.PersonalityTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Application.Feature.PsychologyTests.Queries
{
    public class GetPersonalityTestByIdQuery : MediatR.IRequest<Result<PersonalityTestDto>>
    {
        public int Id { get; set; }
    }
}
