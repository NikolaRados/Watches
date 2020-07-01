using System;
using System.Collections.Generic;
using System.Text;

namespace Watches.Application.DataTransfer
{
    public class CreateOrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public IEnumerable<CreateOrderLineDto> Items { get; set; } = new List<CreateOrderLineDto>();
    }

    public class CreateOrderLineDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
