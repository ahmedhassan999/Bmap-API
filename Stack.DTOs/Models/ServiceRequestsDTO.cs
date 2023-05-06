using Stack.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Models
{
    public class ServiceRequestsDTO
    {
        public long ID { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }

        public string BankName { get; set; }

        public string OfferTitle { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public virtual List<ServiceRequestCommentDTO> Comments { get; set; }

        //public string Note { get; set; }

        //public string RejectionReason { get; set; }

        //// Foreign key for Customer ID
        //public long? CustomerId { get; set; }
        //public virtual CustomerDTO Customer { get; set; }

        //// Foreign key for Services ID
        //public long? ServicesId { get; set; }
        //public virtual ServicesDTO Services { get; set; }

        //// Foreign key for Services types ID
        //public long? ServiceTypesId { get; set; }
        //public virtual ServiceTypesDTO ServiceTypes { get; set; }

        //// Foreign key for Services types ID
        //public long? BanksId { get; set; }
        //public virtual BanksDTO Banks { get; set; }

    }
}
