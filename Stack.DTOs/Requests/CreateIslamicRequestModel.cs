using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    public class CreateIslamicRequestModel
    {

        public DateTime Date { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime TimeToCall { get; set; }

        public string PhoneNumber { get; set; }

        public string Nationality { get; set; }

        public long MonthlySalary { get; set; }

        public string Type { get; set; }

    }
}
