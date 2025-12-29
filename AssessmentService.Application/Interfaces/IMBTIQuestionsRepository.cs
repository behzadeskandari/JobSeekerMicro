using AssessmentService.Domain.Entities;
using JobSeeker.Shared.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentService.Application.Interfaces
{
    public interface IMBTIQuestionsRepository : IWriteRepository<MBTIQuestions>
    {
        Task<IAsyncEnumerable<MBTIQuestions>> GetAllAsync();
        Task<MBTIQuestions> GetByIdAsyncMBTI(Guid id);
        Task AddAsyncMBTI(MBTIQuestions entity);
        void UpdateMBTI(MBTIQuestions entity);
        void DeleteMBTI(MBTIQuestions entity);
    }
}
