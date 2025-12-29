using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssessmentService.Application.Interfaces;
using AssessmentService.Domain.Entities;
using AssessmentService.Persistance.DbContexts;
using AssessmentService.Persistance.GenericRepository;
using JobSeeker.Shared.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AssessmentService.Persistance.Repository
{
    public class MBTIQuestionsRepository : GenericWriteRepository<MBTIQuestions>, IMBTIQuestionsRepository
    {
        private readonly AssessmentDbContext context;

        public MBTIQuestionsRepository(AssessmentDbContext _context) : base(_context)
        {
            context = _context;
        }


        public async Task AddAsyncMBTI(MBTIQuestions entity)
        {

            await context.MBTIQuestions.AddAsync(entity);
        }

        public void DeleteMBTI(MBTIQuestions entity)
        {
            context.MBTIQuestions.Remove(entity);
        }

        public async Task<IAsyncEnumerable<MBTIQuestions>> GetAllAsync()
        {
            return context.MBTIQuestions.AsAsyncEnumerable();
        }

        public async Task<MBTIQuestions> GetByIdAsyncMBTI(Guid id)
        {
            return await context.MBTIQuestions.FirstOrDefaultAsync(x => x.Id == id);

        }

        public void UpdateMBTI(MBTIQuestions entity)
        {
            context.MBTIQuestions.Update(entity);
        }
    }
}
