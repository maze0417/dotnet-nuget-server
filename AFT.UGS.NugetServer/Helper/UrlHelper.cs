using System;
using NuGet.Server;

namespace AFT.UGS.NugetServer.Helper
{
    public class UrlHelper
    {
        public static string GetRepositoryUrl(Uri currentUrl, string applicationPath)
        {
            return Helpers.GetBaseUrl(currentUrl, applicationPath) + "api/v2";
        }

        public static string GetPushUrl(Uri currentUrl, string applicationPath)
        {
            return Helpers.GetBaseUrl(currentUrl, applicationPath) + "api/v2";
        }

        public static string GetBaseUrl(Uri currentUrl, string applicationPath)
        {
            UriBuilder uriBuilder = new UriBuilder(currentUrl);
            string str = uriBuilder.Scheme + "://" + uriBuilder.Host;
            if (uriBuilder.Port != 80 && uriBuilder.Port != 443)
                str = str + (object)":" + (string)(object)uriBuilder.Port;
            return EnsureTrailingSlash(str + applicationPath);
        }

        internal static string EnsureTrailingSlash(string path)
        {
            if (string.IsNullOrEmpty(path) || path.EndsWith("/"))
                return path;
            return path + "/";
        }
    }
}