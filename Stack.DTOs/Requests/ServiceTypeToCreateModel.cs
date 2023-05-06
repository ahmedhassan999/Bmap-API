using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    public class ServiceTypeToCreateModel
    {
        public long ServicesId { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public string Icon { get; set; }

    }
}
