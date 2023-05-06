using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    //DTO for deleteing an object by id . 
    public class CreateCommentModel
    {
        
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public long  ServiceRequestId { get; set; }
       
    }
}
