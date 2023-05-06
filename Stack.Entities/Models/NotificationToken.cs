using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.Entities.Models
{
    public class NotificationToken
    {
        public long ID { get; set; }
        public string Related_Device { get; set; }

        public string Token { get; set; }


    }
}
