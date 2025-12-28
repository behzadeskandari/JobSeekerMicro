using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AdvertisementService.Domain.Common;

namespace AdvertisementService.Domain.Entities
{
    public class Feature : EntityBase<Guid>
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public string IconName { get; set; } = string.Empty;
        
        [Required]
        public string Language { get; set; } = string.Empty;

        public ICollection<int> JobsIds { get; set; } = new List<int>();
    }
}
