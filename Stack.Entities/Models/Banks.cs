using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.Entities.Models
{
    public class Banks
    {
        public long ID { get; set; }

        [Required]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public string Image { get; set; }

        //public virtual List<ServiceRequests> ServiceRequests { get; set; }


    }
}
