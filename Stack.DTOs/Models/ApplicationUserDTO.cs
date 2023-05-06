
using Stack.DTOs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Stack.DTOs.Models
{
    public class ApplicationUserDTO 
    {

        public string ID { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public virtual CustomerDTO Customer { get; set; }

    }

}
