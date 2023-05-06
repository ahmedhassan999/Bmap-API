using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    public class ServiceTypeToEditModel
    {
        public long ID { get; set; }
        public long ServiceID { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }

    }
}
