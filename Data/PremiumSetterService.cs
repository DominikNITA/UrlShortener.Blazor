using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Data
{
    public class PremiumSetterService
    {
        AuthenticationStateProvider _authenticationStateProvider;
        RoleManager<IdentityRole> _roleManager;
        UserManager<IdentityUser> _userManager;

        public PremiumSetterService(AuthenticationStateProvider authenticationStateProvider, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SetPremiumToCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            if(!await _roleManager.RoleExistsAsync("premium"))
            {
                await _roleManager.CreateAsync(new IdentityRole("premium"));
            }
            var identityUser = await _userManager.GetUserAsync(user);
            await _userManager.AddToRoleAsync(identityUser, "premium");
        }
    }
}
