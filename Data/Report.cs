using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Data
{
    public class Report
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public UrlEntry UrlEntry { get; set; }
        [MaxLength(500)]
        public string Reason { get; set; }

        public Report()
        {
            UrlEntry = new UrlEntry();
        }
    }
}
