using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using URLShortner.DataValidators;

namespace URLShortner.Dtos
{
    public class OrignalUrlInput
    {
        [Required]
        //[URLValidator]
        public string Url { get; set; }
    }
}
