using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.Entities.Models
{
    public class ServiceTypes
    {
        public long ID { get; set; }

        [Required]
        public string NameAR { get; set; }       
        public string NameEN { get; set; }       
        public string Icon { get; set; }       
 
        public bool IsDeleted { get; set; }

        // Foreign key for Services ID
        public long? ServicesId { get; set; }
        public virtual Services Services { get; set; }

        public virtual List<Offer> Offers { get; set; }

    }
}
