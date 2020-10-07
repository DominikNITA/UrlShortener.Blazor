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

        public async Task<List<Report>> GetReportsAsync()
        {
            return await _context.Reports.Include(report => report.UrlEntry).ToListAsync();
        }

        public async Task SkipReport(Report report)
        {
            report.IsResolved = true;
            _context.Update(report);
            await _context.SaveChangesAsync();
        }

        public async Task BanFromReport(Report report)
        {
            report.UrlEntry.IsBanned = true;
            report.IsResolved = true;
            _context.Update(report);
            await _context.SaveChangesAsync();
        }
    }
}
