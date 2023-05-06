using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    //DTO for deleteing an object by id . 
    public class BankToEditModel
    {
        public long ID { get; set; }
        public string Name { get; set; }

    }
}
