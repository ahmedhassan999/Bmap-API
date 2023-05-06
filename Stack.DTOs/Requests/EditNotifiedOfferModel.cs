using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    public class EditNotifiedOfferModel
    {

        public long ID { get; set; }
        public string TitleAR { get; set; }
        public string TitleEN { get; set; }

        public string DescriptionAR { get; set; }
        public string DescriptionEN { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime ClientSideDate { get; set; }

    }



}
