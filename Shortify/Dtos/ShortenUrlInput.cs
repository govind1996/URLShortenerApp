using shortify.DataValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shortify.Dtos
{
    public class ShortenUrlInput
    {
        [Required]
        [URLValidator]
        public string Url { get; set; }
    }
}
