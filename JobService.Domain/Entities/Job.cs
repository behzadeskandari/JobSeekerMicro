using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Contracts.Enums;
using JobSeeker.Shared.Kernel.Abstractions;
using JobSeeker.Shared.Models;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace JobService.Domain.Entities
{
    public class Job : IBaseEntity<Guid> , IAggregateRoot
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public JobLevel Level { get; set; }

        // Relations
        [Required]
        public Guid CompanyId { get; set; }
        public bool IsProirity { get; set; }
        public JobType JobType { get; set; }
        public string? JobDescription { get; set; }
        public string? JobRequirment { get; set; }
        //public int? JobRequestsId { get; set; }
        //public JobRequest? JobRequests { get; set; }
        public int? CityId { get; set; }
        public Guid? FeaturesId { get; set; }
        [ForeignKey("TechnicalOption")]
        public int? TechnicalOptionsId { get; set; }
        [ForeignKey("Order")]
        public Guid? OrderId { get; set; }
        [Required]
        [ForeignKey("JobCategory")]
        public int JobCategoryId { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
        public JobOfferStatus Status { get; set; }
        public virtual ICollection<JobApplication> JobApplication { get; set; }
        public ICollection<Guid> CandidatesIds { get; set; }
        public ICollection<JobPost> JobPosts { get; set; }
    }

}
