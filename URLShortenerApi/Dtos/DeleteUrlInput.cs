﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URLShortner.Dtos
{
    public class DeleteUrlInput
    {
        public int UrlId { get; set; }
        public string UserId { get; set; }
    }
}
