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

        public async Task<string> GetRedirectUrl(string shortUrl)
        {
            var temp = await _context.UrlEntries.FirstOrDefaultAsync(url => url.ShortUrl == shortUrl);
            //await Task.Delay(1500);
            if (temp == null) return "";
            return temp.RedirectUrl;
        }
    }
}
