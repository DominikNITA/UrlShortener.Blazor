using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Data
{
    public class UrlEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string ShortUrl { get; set; }
        public string RedirectUrl { get; set; }
        public DateTime? DiscardDate { get; set; }
        public bool ShowAds { get; set; } = false;
        public string OwnerId { get; set; }
        public int VisitCount { get; set; }

        public UrlEntry()
        {

        }
    }

    public class UrlEntryResponse
    {
        public UrlEntry Result { get; set; }
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public UrlEntryResponse(UrlEntry result)
        {
            Result = result;
            IsSuccessful = true;
        }
        public UrlEntryResponse()
        {

        }
    }
}
