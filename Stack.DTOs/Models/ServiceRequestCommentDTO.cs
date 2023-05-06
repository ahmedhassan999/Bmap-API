using Stack.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Models
{
    public class ServiceRequestCommentDTO
    {
        public long ID { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; }

        public long ServiceRequestsId { get; set; }
        public virtual ServiceRequestsDTO ServiceRequests { get; set; }

    }
}
