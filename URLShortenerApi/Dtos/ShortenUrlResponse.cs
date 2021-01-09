using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URLShortner.Dtos
{
    public class ShortenUrlResponse
    {
        public Url Url { get; set; }
        public string Message { get; set; }
    }
}
