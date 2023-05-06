using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests
{
   public class ServiceRequestModel
   {
        public DateTime Date { get; set; }

        public string Note { get; set; }

        public long CustomerId { get; set; }

        public long ServicesId { get; set; }

        public long ServiceTypesId { get; set; }

        public long BanksId { get; set; }


    }

}
