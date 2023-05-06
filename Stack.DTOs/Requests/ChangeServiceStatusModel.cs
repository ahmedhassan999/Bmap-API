using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    public class ChangeServiceStatusModel
    {
        public long ServiceRequestId { get; set; }
        public string RejectionReason { get; set; }


    }
}
