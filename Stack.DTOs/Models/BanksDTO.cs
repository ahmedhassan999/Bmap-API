using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Models
{
    public class BanksDTO
    {
        public long ID { get; set; }

        [Required]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public string Image { get; set; }

        public virtual List<ServiceRequestsDTO> ServiceRequests { get; set; }

    }
}
