using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UrlShortener.Data
{
    public class ShortUrlGenerationService
    {
        ApplicationDbContext _context;
        AuthenticationStateProvider _authenticationStateProvider;

        public ShortUrlGenerationService(ApplicationDbContext context, AuthenticationStateProvider authenticationStateProvider)
        {
            _context = context;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<string> CreateShortUrlAsync(UrlModel urlModel)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if(urlModel.ShortUrl == null || !user.IsInRole("premium"))
            {
                //If redirect url already exists in the database return it's short url. Update creation date
                var temp = await _context.UrlEntries.FirstOrDefaultAsync(url => url.RedirectUrl == urlModel.UrlString);
                if (temp != null)
                {
                    temp.CreationDate = DateTime.Now;
                    _context.Update(temp);
                    await _context.SaveChangesAsync();
                    return temp.ShortUrl;
                }
            }

            var urlEntry = new UrlEntry();
            urlEntry.RedirectUrl = urlModel.UrlString;
            //Generate new short url for the url. If it already exists => retry until success
            do{
                urlEntry.ShortUrl = GenerateShortUrl();
            } while (_context.UrlEntries.Where(url => url.ShortUrl == urlEntry.ShortUrl).Count() > 0);

            await _context.UrlEntries.AddAsync(urlEntry);
            await _context.SaveChangesAsync();
            return urlEntry.ShortUrl;
        }

        private string GenerateShortUrl()
        {
           return Guid.NewGuid().ToString().Substring(0, 5);
        }

        private bool CheckValidUrl(string url)
        {
            if (!Regex.IsMatch(url, @"^https?:\/\/", RegexOptions.IgnoreCase))
                url = "http://" + url;

            if (Uri.TryCreate(url, UriKind.Absolute, out var resultURI))
                return (resultURI.Scheme == Uri.UriSchemeHttp ||
                        resultURI.Scheme == Uri.UriSchemeHttps);

            return false;
        }
    }
}
