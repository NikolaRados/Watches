using System;
using System.Collections.Generic;
using System.Text;

namespace Watches.Domain
{
    public class Brand : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; } 
    }
}
