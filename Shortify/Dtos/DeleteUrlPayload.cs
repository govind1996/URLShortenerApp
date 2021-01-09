using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shortify.Dtos
{
    public class DeleteUrlPayload
    {
        public int UrlId { get; set; }
        public string UserId { get; set; }
    }
}
