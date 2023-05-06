using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Models
{
    public class ServicesDTO
    {
        public long ID { get; set; }

        public string NameAR { get; set; }

        public string NameEN { get; set; }

        public string Header { get; set; }

        public virtual List<ServiceTypesDTO> ServiceTypes { get; set; }

        //public string DescriptionAR { get; set; }

        //public string DescriptionEN { get; set; }

        //public long RequestsCount { get; set; }

        //public string Icon { get; set; }

        //public bool IsDeleted { get; set; }

        //public virtual List<ServiceRequestsDTO> ServiceRequests { get; set; }

    }
}
