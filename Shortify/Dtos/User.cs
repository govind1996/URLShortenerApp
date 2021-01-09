using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shortify.Dtos
{
    public class User
    {
        public string  UserId { get; set; }
        public string JwtToken { get; set; }
        public int ExpiresAt { get; set; }
    }
}
