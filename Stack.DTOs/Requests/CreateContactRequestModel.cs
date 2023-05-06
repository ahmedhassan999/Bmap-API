using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    public class CreateContactRequestModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

        public string Subject { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime RequestDate { get; set; }

    }



}
