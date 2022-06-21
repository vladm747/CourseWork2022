using System;
using System.Collections.Generic;

#nullable disable

namespace SoftwareProductTesting.Models
{
    public  class Message
    {
        public int MessageId { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public int? CommentContentId { get; set; }

        public Comment CommentContent { get; set; }
        public Addressee Receiver { get; set; }
        public Addressee Sender { get; set; }
    }
}
