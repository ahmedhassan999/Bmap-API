using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.Entities.Models
{
    public class OfferBenefit
    {
        public long ID { get; set; }

        [Required]
        public string DescriptionAR { get; set; }

        [Required]
        public string DescriptionEN { get; set; }

        // Foreign key for Service Types
        public long? OfferId { get; set; }
        public virtual Offer Offer { get; set; }

    }
}
