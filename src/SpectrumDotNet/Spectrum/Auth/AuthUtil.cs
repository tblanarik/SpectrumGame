using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum
{
    public static class AuthUtil
    {
        public static List<string> AuthorizedUsers = new List<string>() { "tblanarik@gmail.com" };

        public static async Task<string> GetAuthUser(HttpRequest req, ClaimsPrincipal principal)
        {
            var authInfo = await req.GetAuthInfoAsync();
            return authInfo.UserId;
        }

        public static async Task<AuthResult> IsAuthorized(HttpRequest req, ClaimsPrincipal principal)
        {
            if (!principal.Identity.IsAuthenticated)
            {
                return new AuthResult() { IsAuthenticated = false, Username = string.Empty };
            }
            var user = await GetAuthUser(req, principal);
            return new AuthResult() { IsAuthenticated = true, Username = user };
        }
    }

    public class AuthResult
    {
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }
    }
}
