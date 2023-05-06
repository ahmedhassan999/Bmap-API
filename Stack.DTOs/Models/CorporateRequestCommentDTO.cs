using Stack.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Models
{
    public class CorporateRequestCommentDTO
    {
        public long ID { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; }

        public long CorporateServiceRequestId { get; set; }
        public virtual CorporateServiceRequestDTO CorporateServiceRequest { get; set; }

    }
}
