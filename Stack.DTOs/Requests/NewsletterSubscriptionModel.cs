using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    //DTO for deleteing an object by id . 
    public class NewsletterSubscriptionModel
    {
        public string Email { get; set; }
        public DateTime SubscriptionDate { get; set; }

    }



}
