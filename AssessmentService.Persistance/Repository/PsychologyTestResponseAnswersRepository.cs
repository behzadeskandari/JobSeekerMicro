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
    public class PsychologyTestResponseAnswersRepository : GenericWriteRepository<PsychologyTestResponse>, IPsychologyTestResponseAnswersRepository
    {
        public PsychologyTestResponseAnswersRepository(AssessmentDbContext context) : base(context)
        {
        }
    }
}
