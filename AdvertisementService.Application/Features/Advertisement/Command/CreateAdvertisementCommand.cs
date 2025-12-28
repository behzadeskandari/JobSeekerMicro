using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Advertisement.Command
{
    public class CreateAdvertisementCommand : IRequest<Result<string>>
    {

        public int CompanyId { get; set; }
        public int CategoryId { get; set; }


        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public DateTime JobADVCreatedAt { get; set; } = DateTime.Now;

        public DateTime? ExpiresAt { get; set; }

        public bool IsApproved { get; set; } = false;

        public bool IsPaid { get; set; } = false;
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
    }
}
