using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.Entities.Models
{
    public class ContactRequest
    {
        public long ID { get; set; }

        [Required]
        [StringLength(maximumLength: 20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(maximumLength: 60)]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 255)]
        public string Message { get; set; }

        [Required]
        [StringLength(maximumLength: 40)]
        public string Subject { get; set; }

        [Required]
        [StringLength(maximumLength: 40)]
        public string PhoneNumber { get; set; }

        public DateTime RequestDate { get; set; }
    }

}
