using System;
using System.Collections.Generic;
using System.Text;

namespace Watches.Application.DataTransfer
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
    }
}
