using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using URLShortner.DataValidators;

namespace URLShortner.Dtos
{
    public class ShortenUrlInput
    {
        
        public string UserId { get; set; }
        [Required]
        [URLValidator]
        public string Url { get; set; }
    }
}
