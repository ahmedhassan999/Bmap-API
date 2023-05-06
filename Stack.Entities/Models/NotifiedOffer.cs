using System;
using System.Collections.Generic;

namespace Stack.Entities.Models
{
    public class NotifiedOffer
    {
        public long ID { get; set; }

        public string TitleAR { get; set; }
        public string TitleEN { get; set; }

        public string DescriptionAR { get; set; }
        public string DescriptionEN { get; set; }
        public string Image { get; set; }

        public bool IsInformative { get; set; }
        public string? JobID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual List<NotifiedOfferRequest> NotifiedOfferRequests { get; set; }

    }

}
