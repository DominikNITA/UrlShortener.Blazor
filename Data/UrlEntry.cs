using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Data
{
    public class UrlEntry
    {
        [Key]
        public string ShortUrl { get; set; }
        public string RedirectUrl { get; set; }
        public DateTime CreationDate { get; set; }

        public UrlEntry()
        {
            CreationDate = DateTime.Now;
        }
    }
}
