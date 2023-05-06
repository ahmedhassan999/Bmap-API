using Stack.DTOs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Models
{
    public class OfferBenefitDTO
    {
        public long ID { get; set; }

        [Required]
        public string DescriptionAR { get; set; }

        [Required]
        public string DescriptionEN { get; set; }

        // Foreign key for Service Types
        public long? OfferId { get; set; }
        public virtual OfferDTO Offer { get; set; }

    }
}
