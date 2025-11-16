using JobSeeker.Shared.Common.Interfaces;
using JobService.Domain.Entities;

namespace JobService.Application.Interfaces
{
    public interface IJobPostsRepository :
        IWriteRepository<JobPost>
    {
        Task<IEnumerable<JobPost>> GetAllWithSkillsAsync();
        Task<JobPost> GetByIdWithSkillsAsync(Guid jobId);
    }
}