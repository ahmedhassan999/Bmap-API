using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    public class ServiceRequestToCreateModel
    {
        public DateTime Date { get; set; }

        public string Status { get; set; }

        public string BankName { get; set; }

        public string OfferTitle { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

    }
}
