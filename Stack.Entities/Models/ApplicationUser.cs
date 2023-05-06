
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Stack.Entities.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Customer Customer { get; set; }

        public string Image { get; set; }

    }
}
