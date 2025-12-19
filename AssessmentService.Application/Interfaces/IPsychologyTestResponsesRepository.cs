using AssessmentService.Domain.Entities;
using JobSeeker.Shared.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentService.Application.Interfaces
{
    public interface IPsychologyTestResponsesRepository : IWriteRepository<PsychologyTestResponse>
    {
    }
}
