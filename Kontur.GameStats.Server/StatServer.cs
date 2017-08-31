using Owin;
using System.Web.Http;


namespace Kontur.GameStats.Server
{
    class StatServer
    {

        public void Configuration(IAppBuilder appBuilder)
        {


            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "info",
                routeTemplate: "{controller}/info"
            );

            config.Routes.MapHttpRoute(
                name: "endpoint_info",
                routeTemplate: "{controller}/{endpoint}/{action}"
            );
      
            config.Routes.MapHttpRoute(
                name: "matches",
                routeTemplate: "{controller}/{endpoint}/{action}/{timestamp}"
            );


            config.Routes.MapHttpRoute(
                name: "stat_player",
                routeTemplate: "{controller}/{name}/{action}"
            );
            config.Routes.MapHttpRoute(
                name: "reports",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
                        
        }
    }
}
