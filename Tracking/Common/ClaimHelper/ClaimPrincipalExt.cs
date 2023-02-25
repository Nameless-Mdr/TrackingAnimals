using System.Security.Claims;
using Microsoft.VisualBasic.CompilerServices;

namespace Common;

public static class ClaimPrincipalExt
{
    public static T? GetClaimValue<T>(this ClaimsPrincipal user, string claim)
    {
        var value = user.Claims.FirstOrDefault(x => x.Type == claim)?.Value;

        if (value != null)
            return Utils.Convert<T>(value);

        return default;

    }
}