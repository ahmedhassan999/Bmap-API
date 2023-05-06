using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.Entities.Models
{
    public class Services
    {
        public long ID { get; set; }

        [Required]
        public string NameAR { get; set; }

        [Required]
        public string NameEN { get; set; }

        public string Header { get; set; }

        public virtual List<ServiceTypes> ServiceTypes { get; set; }

    }
}
