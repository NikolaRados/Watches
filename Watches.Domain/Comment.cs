using System;
using System.Collections.Generic;
using System.Text;

namespace Watches.Domain
{
    public class Comment : Entity
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
