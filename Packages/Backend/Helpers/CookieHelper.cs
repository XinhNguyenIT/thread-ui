using Backend.Enums;

namespace Backend.Helpers;

public static class CookieHelper
{
    public static void SetSecureCookie(HttpResponse response, string name, string token, DateTime expires)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Expires = expires
        };

        response.Cookies.Append(name, token, cookieOptions);
    }

    public static void ClearCookie(HttpResponse response)
    {
        response.Cookies.Delete("access");
        response.Cookies.Delete("refresh");
    }
}