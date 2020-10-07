using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Data
{
    public class RedirectService
    {
        ApplicationDbContext _context;
        public RedirectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UrlEntryResponse> GetRedirectUrl(string shortUrl)
        {
            var temp = await _context.UrlEntries.FirstOrDefaultAsync(url =>
                url.ShortUrl == shortUrl &&
                (!url.DiscardDate.HasValue || url.DiscardDate.Value.CompareTo(DateTime.Now) > 0) &&
                !url.IsBanned
            );

            if (temp == null) return new UrlEntryResponse() { IsSuccessful = false, ErrorMessage = "This link does not exist in our database. Please get in touch with the sender. Redirecting to the home page..." };
            temp.VisitCount++;
            await _context.SaveChangesAsync();

            return new UrlEntryResponse(temp);
        }
    }
}
