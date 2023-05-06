using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Stack.DTOs.Models;

namespace Stack.DTOs.Requests
{
    public class SearchResult
    {
        public List<OfferDTO> Offers { get; set; }
        public List<ServiceTypesDTO> ServiceTypes { get; set; }

    }
}
