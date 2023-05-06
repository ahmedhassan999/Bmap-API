using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    public class ServiceToEditModel
    {
        public long ID { get; set; }
        public string NameAR { get; set; }
        public string DescriptionAR { get; set; }

        public string NameEN { get; set; }
        public string DescriptionEN { get; set; }


    }
}
