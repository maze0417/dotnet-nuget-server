using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using AFT.UGS.NugetServer;
using NuGet.Server;
using NuGet.Server.V2;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NuGetODataConfig), "Start")]

namespace AFT.UGS.NugetServer
{
    public static class NuGetODataConfig
    {
        public static void Start()
        {
            ServiceResolver.SetServiceResolver(new DefaultServiceResolver());

            var config = GlobalConfiguration.Configuration;

            config.UseNuGetV2WebApiFeed("NuGetDefault", "api/v2", "PackagesOData");
            config.UseNuGetV2WebApiFeed("NuGetDefault2", "nuget", "PackagesOData");

            config.Routes.MapHttpRoute(
                name: "NuGetDefault_ClearCache",
                routeTemplate: "nuget/clear-cache",
                defaults: new { controller = "PackagesOData", action = "ClearCache" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );
            config.Routes.MapHttpRoute(
              name: "UploadPackage",
              routeTemplate: "api/v2/package",
              defaults: new { controller = "PackagesOData", action = "UploadPackage" },
              constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) }
          );
        }
    }
}