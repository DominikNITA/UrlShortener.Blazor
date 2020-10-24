using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Data
{
    public class UrlModel
    {
        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string UrlString { get; set; }
        [StringLength(10, MinimumLength = 3)]
        [RegularExpression("^[a-zA-Z0-9]*$")]
        public string ShortRelativeUrl{ get; set; }
    }
}
