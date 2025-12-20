using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobSeeker.Shared.Contracts.Resume
{
    public class UpdateResumeRequestDto
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public string FullName { get; set; }
        
        [Required, EmailAddress]
        public string Email { get; set; }
        
        [Required, Phone]
        public string Phone { get; set; }
        
        [Required]
        public string Address { get; set; }
        
        [Required]
        public string ProfilePictureUrl { get; set; }
        
        [Required]
        public string Summary { get; set; }
        
        public ICollection<WorkExperienceDto> WorkExperiences { get; set; } = new List<WorkExperienceDto>();
        public ICollection<EducationDto> Educations { get; set; } = new List<EducationDto>();
        public ICollection<SkillDto> Skills { get; set; } = new List<SkillDto>();
        public ICollection<LanguageDto> Languages { get; set; } = new List<LanguageDto>();
    }
}
