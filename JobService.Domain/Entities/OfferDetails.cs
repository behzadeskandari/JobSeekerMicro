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

    public class OfferDetails : IBaseEntity<Guid>
    {
        [Key]
        [ForeignKey("JobApplication")]
        public Guid ApplicationId { get; set; }
        public virtual JobApplication JobApplication { get; set; }

        //[ForeignKey("OfferedBy")]
        //public Guid OfferedById { get; set; }
        //// Assuming you have a User entity for who made the offer
        //// public virtual User OfferedBy { get; set; }

        public DateTime OfferDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }
        public string Currency { get; set; }
        public string Benefits { get; set; }
        public string Status { get; set; }
        public Guid Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
    }
}
