using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    //DTO for deleteing an object by id . 
    public class CustomerToCreateModel
    {
        
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        
        public string NationalID { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public string JobTitle { get; set; }
        
        public string Type { get; set; }
        
        public string AccountStatus { get; set; }
        
        public string City { get; set; }
        
        public string Province { get; set; }
        
        public string Country { get; set; }
        
        public string Street { get; set; }
        
        public string NationalIdFront { get; set; }
        
        public string NationalIdBack { get; set; }
        
        public string ProfilePicture { get; set; }
        
        public string First { get; set; }
        
        public string FirstMiddle { get; set; }
        
        public string SecondMiddle { get; set; }
        
        public string Last { get; set; }
        [Required]
        [MaxLength(256)]
        [MinLength(6)]
        public string Password { get; set; }


    }
}
