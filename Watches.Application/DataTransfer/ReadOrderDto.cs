using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watches.Application.DataTransfer
{
    public class ReadOrderDto
    {
        public int Id { get; set; }
        public string UserInfo { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<ReadOrderLineDto> OrderLines { get; set; } = new List<ReadOrderLineDto>();
        public decimal TotalPrice => OrderLines.Sum(x => x.Price * x.Quantity);
    }

    public class ReadOrderLineDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
