using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.Entities.Models
{
    public class IslamicServiceRequest
    {
        public long ID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime TimeToCall { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Nationality { get; set; }

        [Required]
        public long MonthlySalary { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Status { get; set; }


        public virtual List<IslamicRequestComment> Comments { get; set; }


    }
}
