using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models
{
    public class NotifiedOfferRequestDTO
    {
        public long ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime RequestDate { get; set; }

        public long NotifiedOfferId { get; set; }
        public virtual NotifiedOfferDTO NotifiedOffer { get; set; }
    }
}
