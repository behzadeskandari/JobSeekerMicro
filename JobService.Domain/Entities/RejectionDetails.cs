﻿using JobSeeker.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobService.Domain.Entities
{
    public class RejectionDetails : IBaseEntity<Guid>
    {
        [Key]
        [ForeignKey("JobApplication")]
        public Guid ApplicationId { get; set; }
        public virtual JobApplication JobApplication { get; set; }

        [ForeignKey("RejectedBy")]
        public Guid RejectedById { get; set; }
        // Assuming you have a User entity for who rejected the application
        // public virtual User RejectedBy { get; set; }

        public DateTime RejectionDate { get; set; }
        public string Reason { get; set; }
        public Guid Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
    }
}
