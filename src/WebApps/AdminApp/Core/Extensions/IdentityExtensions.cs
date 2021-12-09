using IdentityModel;
using System.Linq;
using System.Security.Claims;

namespace AdminApp.Core.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = ((ClaimsPrincipal)claimsPrincipal.Identity).Claims.SingleOrDefault(i =>
               i.Type == JwtClaimTypes.Subject);
            return claim.Value;
        }

        public static string GetFullName(this ClaimsPrincipal claimsPrincipal)
        {
            var firstName =
                ((ClaimsPrincipal)claimsPrincipal.Identity).Claims.SingleOrDefault(i =>
                   i.Type == JwtClaimTypes.GivenName);

            var lastName = ((ClaimsPrincipal)claimsPrincipal.Identity).Claims.SingleOrDefault(i => i.Type == JwtClaimTypes.FamilyName);
            return firstName?.Value + lastName?.Value;
        }
    }
}
