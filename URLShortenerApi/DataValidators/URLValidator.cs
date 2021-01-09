using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace URLShortner.DataValidators
{
    public class URLValidator: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            String Url = Convert.ToString(value);
            if (Uri.IsWellFormedUriString(Url, UriKind.Absolute))
                return true;
            return false;
        }
    }
}
