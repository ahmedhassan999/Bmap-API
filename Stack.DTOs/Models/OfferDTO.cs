using Stack.DTOs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Models
{
    public class OfferDTO
    {
        public long ID { get; set; }

        [Required]
        public string NameAR { get; set; }

        [Required]
        public string NameEN { get; set; }

        [Required]
        public string BankName { get; set; }

        [Required]
        public string Icon { get; set; }

        [Required]
        public string MinimumSalary { get; set; }

        [Required]
        public string Rate { get; set; }

        [Required]
        public string AnnualFee { get; set; }

        public string JoiningOffersAR { get; set; }
        public string JoiningOffersEN { get; set; }
        public string KeyFeaturesAR { get; set; }
        public string KeyFeaturesEN { get; set; }
        public string RewardFeaturesAR { get; set; }
        public string RewardFeaturesEN { get; set; }
        public string ThingsToBeAwareOfAR { get; set; }
        public string ThingsToBeAwareOfEN { get; set; }

        // Foreign key for Service Types
        public long? ServiceTypesId { get; set; }
        public virtual ServiceTypesDTO ServiceTypes { get; set; }

        public virtual List<OfferBenefitDTO> OfferBenefits { get; set; }

    }
}
