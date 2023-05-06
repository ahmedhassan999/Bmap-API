using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models
{
    public class NewsletterSubscriptionDTO
    {
        public long ID { get; set; }
        public string Email { get; set; }
        public DateTime SubscriptionDate { get; set; }

    }

}
