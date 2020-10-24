using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Data
{
    public class UrlResponse
    {
        public string Result { get; set; }
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }

        public UrlResponse(string shortRelativeUrl)
        {
            Result = shortRelativeUrl;
            IsSuccessful = true;
        }
        public UrlResponse()
        {

        }
    }
}
