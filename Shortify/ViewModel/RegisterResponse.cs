using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shortify.ViewModel
{
    public class RegisterResponse
    {
        public string  Username { get; set; }
        public string Email { get; set; }
        public ApiException exception { get; set; }
    }
    public class ApiException
    {
        public List<string> Errors { get; set; }
    }
}
