using System.Security.Claims;

namespace Common.ClaimHelper;

public static class ClaimPrincipalExt
{
    public static T? GetClaimValue<T>(this ClaimsPrincipal user, string claim)
    {
        var value = user.Claims.FirstOrDefault(x => x.Type == claim)?.Value;

        if (value != null)
            return value.Convert<T>();

        return default;

    }
}