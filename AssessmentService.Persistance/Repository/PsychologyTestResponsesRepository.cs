using AssessmentService.Application.Interfaces;
using AssessmentService.Domain.Entities;
using AssessmentService.Persistance.DbContexts;
using AssessmentService.Persistance.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentService.Persistance.Repository
{
    public class PsychologyTestResponsesRepository : GenericWriteRepository<PsychologyTestResponse>, IPsychologyTestResponsesRepository
    {
        public PsychologyTestResponsesRepository(AssessmentDbContext context) : base(context)
        {
        }
    }
}
