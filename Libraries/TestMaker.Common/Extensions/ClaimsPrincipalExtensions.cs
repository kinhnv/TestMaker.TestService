using System.Security.Claims;

namespace TestMaker.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid? GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var userIdAsString = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? null;

            var flag = Guid.TryParse(userIdAsString, out Guid userId);

            return flag ? userId : null;
        }
    }
}
