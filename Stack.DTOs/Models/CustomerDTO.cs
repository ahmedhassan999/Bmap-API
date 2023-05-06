using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Models
{
    public class CustomerDTO
    {
        public long ID { get; set; }
        [Required]
        public string Gender { get; set; }       
        [Required]
        public string NationalID { get; set; }       
        [Required]
        public DateTime DateOfBirth { get; set; }       
        [Required]
        public string JobTitle { get; set; }       
        [Required]
        public string Type { get; set; }       
        [Required]
        public string AccountStatus { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string NationalIdFront { get; set; }
        [Required]
        public string NationalIdBack { get; set; }
        [Required]
        public string ProfilePicture { get; set; }
        [Required]
        public string First { get; set; }
        [Required]
        public string FirstMiddle { get; set; }
        [Required]
        public string SecondMiddle { get; set; }
        [Required]
        public string Last { get; set; }
        public bool IsDeleted { get; set; }

        // Foreign key for User ID
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUserDTO ApplicationUser { get; set; }
        public virtual List<ServiceRequestsDTO> ServiceRequests { get; set; }

    }
}
