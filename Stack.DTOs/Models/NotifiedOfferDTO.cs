using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models
{
    public class NotifiedOfferDTO
    {
        public long ID { get; set; }

        public string TitleAR { get; set; }
        public string TitleEN { get; set; }

        public string DescriptionAR { get; set; }
        public string DescriptionEN { get; set; }
        public string Image { get; set; }

        public bool IsInformative { get; set; }
        public long? JobID { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual List<NotifiedOfferRequestDTO> NotifiedOfferRequests { get; set; }

    }
}
