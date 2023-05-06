using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.Entities.Models
{
    public class ServiceRequests
    {
        public long ID { get; set; }

        [Required]
        public DateTime Date { get; set; }  
        
        [Required]
        public string Status { get; set; }

        [Required]
        public string BankName { get; set; }

        [Required]
        public string OfferTitle { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public virtual List<ServiceRequestComment> Comments { get; set; }

    }
}
