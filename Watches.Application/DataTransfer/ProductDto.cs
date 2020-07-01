using System;
using System.Collections.Generic;
using System.Text;

namespace Watches.Application.DataTransfer
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public string Gender { get; set; }
        public string Movement { get; set; }
        public string Glass { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
