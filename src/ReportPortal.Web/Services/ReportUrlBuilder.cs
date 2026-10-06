using System.Collections.Generic;
using System.Configuration;
using System;
using System.Web;

namespace ReportPortal.Web.Services
{
    public class ReportUrlBuilder
    {
        public string BuildReportUrl(string reportServerPath, IDictionary<string, string> parameters)
        {
            if (IsAbsoluteWebUrl(reportServerPath))
            {
                return AppendParameters(reportServerPath, parameters);
            }

            var baseUrl = (ConfigurationManager.AppSettings["ReportServerBaseUrl"] ?? string.Empty).TrimEnd('/', '?');
            var path = reportServerPath ?? string.Empty;
            var url = baseUrl + "?" + path.TrimStart('?') + "&rs:Command=Render";

            return AppendParameters(url, parameters);
        }

        private static bool IsAbsoluteWebUrl(string value)
        {
            return !string.IsNullOrWhiteSpace(value)
                && (value.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
                    || value.StartsWith("http://", StringComparison.OrdinalIgnoreCase));
        }

        private static string AppendParameters(string url, IDictionary<string, string> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return url;
            }

            var separator = url.Contains("?") ? "&" : "?";
            foreach (var parameter in parameters)
            {
                url += separator + HttpUtility.UrlEncode(parameter.Key) + "=" + HttpUtility.UrlEncode(parameter.Value);
                separator = "&";
            }

            return url;
        }
    }
}