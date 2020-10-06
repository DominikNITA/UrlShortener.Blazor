using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Data
{
    public class ReportService
    {
        ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SendReport(Report report)
        {
            var urlEntry = await _context.UrlEntries.FirstOrDefaultAsync(url => url.ShortUrl == report.UrlEntry.ShortUrl);

            if (urlEntry == null) return false;

            report.UrlEntry = urlEntry;
            await _context.AddAsync(report);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
