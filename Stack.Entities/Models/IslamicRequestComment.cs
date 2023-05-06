using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.Entities.Models
{
    public class IslamicRequestComment
    {
        public long ID { get; set; }

        [Required]
        public DateTime Date { get; set; }  

        public string Comment { get; set; }

        public long IslamicServiceRequestId { get; set; }
        public virtual IslamicServiceRequest IslamicServiceRequest { get; set; }

    }
}
