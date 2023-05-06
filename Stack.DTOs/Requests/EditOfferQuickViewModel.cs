using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    //DTO for deleteing an object by id . 
    public class EditOfferQuickViewModel
    {
        public long ID { get; set; }
        public string JoiningOffersAR { get; set; }
        public string JoiningOffersEN { get; set; }
        public string KeyFeaturesAR { get; set; }
        public string KeyFeaturesEN { get; set; }
        public string RewardFeaturesAR { get; set; }
        public string RewardFeaturesEN { get; set; }
        public string ThingsToBeAwareOfAR { get; set; }
        public string ThingsToBeAwareOfEN { get; set; }

    }
}
