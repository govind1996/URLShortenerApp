using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shortify.Dtos
{
    public class UrlListResponse
    {
        public List<Url> UrlList { get; set; }
        public string Error { get; set; }
    }
}
