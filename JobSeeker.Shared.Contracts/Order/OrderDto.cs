using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Contracts.Order
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public List<SalesOrderItemDto> SalesOrderItems { get; set; }
        public bool IsPaid { get; set; }
    }
}
