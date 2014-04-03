using System.Web.Hosting;
using System.Web.Http;

using Compass.Common.IO.ShapeFile;
using Compass.Tessera.Core;
using Compass.Tessera.IO;

namespace TileServer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
        }

        public static void ConfigureApi()
        {
            TinyIoC.TinyIoCContainer.Current.AutoRegister();

           // TinyIoC.TinyIoCContainer.Current.Register<ICacheManager>(new CacheManager());//,CacheManager>().AsSingleton();
            
            var cacheManager = TinyIoC.TinyIoCContainer.Current.Resolve<CacheManager>();
            cacheManager.BasePath = HostingEnvironment.MapPath("~/");
            
            //TinyIoC.TinyIoCContainer.Current.Register<IRenderer>(new Renderer());
            TinyIoC.TinyIoCContainer.Current.Register<ShapeFileManager>();

            TinyIoC.TinyIoCContainer.Current.Register<TileGenerator>().AsSingleton();
            var tileGenerator = TinyIoC.TinyIoCContainer.Current.Resolve<TileGenerator>();
            tileGenerator.Setup(HostingEnvironment.MapPath("~/Maps"), "roads.shp");
        }

    }
}