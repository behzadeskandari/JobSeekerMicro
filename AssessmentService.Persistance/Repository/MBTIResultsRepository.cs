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
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

namespace AssessmentService.Persistance.Repository
{
    public class MBTIResultsRepository : GenericWriteRepository<MBTIResult>, IMBTIResultsRepository
    {
        private readonly AssessmentDbContext context;
        public MBTIResultsRepository(AssessmentDbContext _context) : base(_context)
        {
            context = _context;
        }
        public async Task AddAsyncMBTI(MBTIResult entity)
        {
            await context.MBTIResults.AddAsync(entity);
        }
        public void DeleteMBTI(MBTIResult entity)
        {
            context.MBTIResults.Remove(entity);
        }
        public async Task<IEnumerable<MBTIResult>> GetAllAsyncMBTI()
        {
            return await context.MBTIResults.ToListAsync();
        }
        public async Task<MBTIResult> GetByIdAsyncMBTI(Guid id)
        {
            return await context.MBTIResults.FirstOrDefaultAsync(x => x.Id == id);
        }
        public void UpdateMBTI(MBTIResult entity)
        {
            context.MBTIResults.Update(entity);
        }
    }
}
