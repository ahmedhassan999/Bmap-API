using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    public class CreateOfferBenefitModel
    {

        public string DescriptionAR { get; set; }

        public string DescriptionEN { get; set; }

        public long OfferId { get; set; }

    }



}
