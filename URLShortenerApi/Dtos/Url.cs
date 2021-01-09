using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace URLShortner.Dtos
{
    public class Url
    {
        public int Id { get; set; }
        public string OrignalUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public long Clicks { get; set; }
        public DateTime LastClicked { get; set; }
        public string Title { get; set; }
    }
}
