using System;
using System.Collections.Generic;

#nullable disable

namespace SoftwareProductTesting.Models
{
    public  class Comment
    {
        public Comment()
        {
            Messages = new HashSet<Message>();
        }

        public int CommentId { get; set; }
        public string Content { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
