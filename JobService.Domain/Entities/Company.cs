using System;
using System.Collections.Generic;
using JobSeeker.Shared.Contracts.Enums;
using JobService.Domain.Common;

namespace JobService.Domain.Entities
{
    public class Company : AuditableEntityBaseInt
    {
        public string Name { get; set; } = string.Empty;
        public CompanySize Size { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public string Industry { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public DateTime FoundedDate { get; set; }
        public bool IsVerified { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string UserId { get; set; }
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
        public ICollection<int> AdvertisementsIds { get; set; }
        public ICollection<CompanyBenefit> Benefits { get; set; } = new List<CompanyBenefit>();

        public int? JobCategoryId { get; set; }
        public int? CityId { get; set; }
        public int? ProvinceId { get; set; }
        public decimal Rating { get; set; }
        public string LogoUrl { get; set; } = string.Empty;

    }
}
