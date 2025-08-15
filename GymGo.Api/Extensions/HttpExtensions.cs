using System.Globalization;
using System.Net.Http.Headers;

namespace GymGo.Api.Extensions
{
    public static class HttpExtensions
    {
        public static CultureInfo GetPreferredCulture(this HttpContext context)
        {
            var preferredLanguage = context.Request.Headers.AcceptLanguage.FirstOrDefault();
            var cultureName = StringWithQualityHeaderValue.TryParse(preferredLanguage, out var parsedValue)
                ? parsedValue.Value
                : null;

            return cultureName is not null ? new CultureInfo(cultureName) : CultureInfo.CurrentUICulture;
        }
    }
}
