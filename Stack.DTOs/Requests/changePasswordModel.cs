using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    //Basic Registration Model . 
    public class changePasswordModel
    {

        public string AccountID { get; set; }
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

    }
}
