using System.Collections.Generic;
using System.Configuration;
using System.Web;

namespace ReportPortal.Web.Services
{
    public class ReportUrlBuilder
    {
        public string BuildReportUrl(string reportServerPath, IDictionary<string, string> parameters)
        {
            var baseUrl = (ConfigurationManager.AppSettings["ReportServerBaseUrl"] ?? string.Empty).TrimEnd('/', '?');
            var path = reportServerPath ?? string.Empty;
            var url = baseUrl + "?" + path.TrimStart('?') + "&rs:Command=Render";

            if (parameters == null)
            {
                return url;
            }

            foreach (var parameter in parameters)
            {
                url += "&" + HttpUtility.UrlEncode(parameter.Key) + "=" + HttpUtility.UrlEncode(parameter.Value);
            }

            return url;
        }
    }
}