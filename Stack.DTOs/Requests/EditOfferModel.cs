using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    public class EditOfferModel
    {

        public long ID { get; set; }
        public string NameAR { get; set; }

        public string NameEN { get; set; }

        public string BankName { get; set; }

        public string MinimumSalary { get; set; }

        public string Rate { get; set; }

        public string AnnualFee { get; set; }

    }



}
