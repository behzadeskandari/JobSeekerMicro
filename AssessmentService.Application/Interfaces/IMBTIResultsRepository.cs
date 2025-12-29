using AssessmentService.Domain.Entities;
using JobSeeker.Shared.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentService.Application.Interfaces
{
    public interface IMBTIResultsRepository : IWriteRepository<MBTIResult>
    {
        Task<IEnumerable<MBTIResult>> GetAllAsyncMBTI();
        Task<MBTIResult> GetByIdAsyncMBTI(Guid id);
        Task AddAsyncMBTI(MBTIResult entity);
        void UpdateMBTI(MBTIResult entity);
        void DeleteMBTI(MBTIResult entity);
    }
}
