using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    public class CreateNotifiedOfferModel
    {

        public string TitleAR { get; set; }
        public string TitleEN { get; set; }

        public string DescriptionAR { get; set; }
        public string DescriptionEN { get; set; }
        public string Image { get; set; }


        public bool IsInformative { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }



}
