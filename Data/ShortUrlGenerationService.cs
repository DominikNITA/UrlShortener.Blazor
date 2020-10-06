using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
        List<string> _reservedShortLinks = new List<string>() { "report" };

        public ShortUrlGenerationService(ApplicationDbContext context, AuthenticationStateProvider authenticationStateProvider)
        {
            _context = context;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<UrlResponse> CreateShortUrlAsync(UrlModel urlModel)
        {
            if (!CheckValidUrl(urlModel.UrlString))
            {
                return new UrlResponse() { IsSuccessful = false };
            }
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            DateTime? discardDate = GetDiscardDateFromUserType(user);

            //if(urlModel.ShortRelativeUrl == null || !user.IsInRole("premium"))
            //{
            //    //If redirect url already exists in the database return it's short url. Update creation date
            //    var temp = await _context.UrlEntries.FirstOrDefaultAsync(url => url.RedirectUrl == urlModel.UrlString);
            //    if (temp != null)
            //    {
            //        temp.DiscardDate = discardDate;
            //        _context.Update(temp);
            //        await _context.SaveChangesAsync();
            //        return new UrlResponse(temp.ShortUrl);
            //    }
            //}

            var urlEntry = new UrlEntry();
            urlEntry.DiscardDate = discardDate;
            urlEntry.RedirectUrl = urlModel.UrlString;

            if(urlModel.ShortRelativeUrl != null && user.IsInRole("premium"))
            {
                if (_reservedShortLinks.Contains(urlModel.ShortRelativeUrl))
                {
                    return new UrlResponse() { IsSuccessful = false };
                }
                if(_context.UrlEntries.Where(url => url.ShortUrl == urlModel.ShortRelativeUrl).Any())
                {
                    return new UrlResponse() { IsSuccessful = false };
                }
                else
                {
                    urlEntry.ShortUrl = urlModel.ShortRelativeUrl;
                }
            }
            else
            {
                //Generate new short url for the url. If it already exists => retry until success
                do
                {
                    urlEntry.ShortUrl = GenerateShortUrl();
                } while (_context.UrlEntries.Where(url => url.ShortUrl == urlEntry.ShortUrl).Any() || _reservedShortLinks.Contains(urlEntry.ShortUrl));
            }

            urlEntry.OwnerId = user.Identity.Name;
            if (!user.Identity.IsAuthenticated)
            {
                urlEntry.ShowAds = true;
                urlEntry.OwnerId = "";
            }

            _context.UrlEntries.Add(urlEntry);
            await _context.SaveChangesAsync();

            return new UrlResponse(urlEntry.ShortUrl);
        }

        private DateTime? GetDiscardDateFromUserType(System.Security.Claims.ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
            {
                return DateTime.Now.AddDays(7);
            }
            if (user.IsInRole("premium"))
            {
                return null;
            }
            return DateTime.Now.AddYears(1);
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
