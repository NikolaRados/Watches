using System;
using System.Collections.Generic;
using System.Text;

namespace Watches.Domain
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public Gender Gender { get; set; }
        public Movement Movement { get; set; }
        public Glass Glass { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; } = new HashSet<OrderLine>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }

    public enum Gender
    {
        Male,
        Female
    }

    public enum Movement
    {
        Quartz,
        Mechanical,
        Automatic
    }

    public enum Glass
    {
        Acrylic,
        Mineral,
        Sapphire
    }
}
