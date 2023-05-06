using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.Entities.Models
{
    public class NewsletterSubscription
    {
        public long ID { get; set; }

        [Required]
        [StringLength(maximumLength: 60)]
        public string Email { get; set; }
        public DateTime SubscriptionDate { get; set; }

    }
}
