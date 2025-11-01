﻿using JobSeeker.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobService.Domain.Entities
{
    public class JobCategory : IBaseEntity<int>
    {

        public int Id { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;
        public string Name { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string Slug { get; set; }
        public string Industry { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public bool? IsActive { get; set; }

        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
