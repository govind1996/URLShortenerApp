using shortify.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shortify.ViewModel
{
    public class LoginResponse
    {
        public User user { get; set; }
        public LoginException Exception { get; set; }
    }
    public class LoginException
    {
        public string Message { get; set; }
    }
}
