using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    //DTO for deleteing an object by id . 
    public class EditAdminImageModel
    {
        public string ID { get; set; }
        public string Image { get; set; }

    }
}
