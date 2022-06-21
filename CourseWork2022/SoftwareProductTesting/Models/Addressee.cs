using System;
using System.Collections.Generic;

#nullable disable

namespace SoftwareProductTesting.Models
{
    public  class Addressee
    {
        public Addressee()
        {
            MessageReceivers = new HashSet<Message>();
            MessageSenders = new HashSet<Message>();
        }

        public int PersonId { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }

        public ICollection<Message> MessageReceivers { get; set; }
        public ICollection<Message> MessageSenders { get; set; }
    }
}
