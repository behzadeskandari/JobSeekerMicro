using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Contracts.City
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
        public int? ProvinceId { get; set; }
        public bool IsActive { get; set; }
    }
}
