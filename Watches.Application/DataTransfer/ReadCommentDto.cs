using System;
using System.Collections.Generic;
using System.Text;

namespace Watches.Application.DataTransfer
{
    public class ReadCommentDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
    }
}
