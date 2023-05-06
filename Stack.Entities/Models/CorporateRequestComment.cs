using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.Entities.Models
{
    public class CorporateRequestComment
    {
        public long ID { get; set; }

        [Required]
        public DateTime Date { get; set; }  

        public string Comment { get; set; }

        public long CorporateServiceRequestId { get; set; }
        public virtual CorporateServiceRequest CorporateServiceRequest { get; set; }

    }
}
