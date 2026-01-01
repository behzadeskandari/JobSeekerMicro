using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Kernel.Abstractions;
using JobSeeker.Shared.Kernel.Domain;
using JobSeeker.Shared.Models.Roles;
using MassTransit.Transports;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Domain.Entities
{
    public class User : IdentityUser ,IAggregateRoot
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public override string? Email { get; set; }
        public string Password { get; set; } = string.Empty;        
        public bool? IsActive { get; set; }
        public string PictureUrl { get; set; } = string.Empty;
        public string Role { get; set; } = AppRoles.User;

        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }

        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;

        //public int JobOffersId { get; set; }

        //public int JobPostId { get; set; }
        // Navigation properties
        //public ICollection<Advertisement> Advertisements { get; set; }
        //public ICollection<Candidate> Candidates { get; set; }
        //public ICollection<JobPost> JobPosts { get; set; }
        //public ICollection<JobRequest> JobRequests { get; set; }
        //public ICollection<Job> Jobs { get; set; }
        //public MBTIResult MBTIResult { get; set; }
        //public ICollection<Order> Orders { get; set; }
        //public ICollection<Payment> Payments { get; set; }
        //public ICollection<Resume> Resumes { get; set; }
        //public ICollection<PsychologyTestResponse> PsychologyTestResponses { get; set; }
        //public ICollection<PsychologyTestResult> PsychologyTestResults { get; set; }
        //public ICollection<PersonalityTestResponse> PersonalityTestResponses { get; set; }
        //public ICollection<PersonalityTestResult> PersonalityTestResults { get; set; }
        [NotMapped]
        public string RedirectUrl { get; set; }


        private readonly List<DomainEvent> _domainEvents = new();

        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

    }

}
