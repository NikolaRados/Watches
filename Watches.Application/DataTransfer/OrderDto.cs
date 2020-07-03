using System;
using System.Collections.Generic;
using System.Text;
using Watches.Domain;

namespace Watches.Application.DataTransfer
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }

    }
}
