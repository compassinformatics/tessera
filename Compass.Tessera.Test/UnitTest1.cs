using System.Threading.Tasks;

using Compass.Common.IO.ShapeFile;
using Compass.Tessera.Core;
using Compass.Tessera.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Compass.Tessera.Test
{
    /// <summary>
    /// Unit test class
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            TinyIoC.TinyIoCContainer.Current.AutoRegister();

            // TinyIoC.TinyIoCContainer.Current.Register<ICacheManager>(new CacheManager());//,CacheManager>().AsSingleton();

            var cacheManager = TinyIoC.TinyIoCContainer.Current.Resolve<CacheManager>();
            cacheManager.BasePath = string.Empty;

            //TinyIoC.TinyIoCContainer.Current.Register<IRenderer>(new Renderer());
            TinyIoC.TinyIoCContainer.Current.Register<ShapeFileManager>();

            TinyIoC.TinyIoCContainer.Current.Register<TileGenerator>().AsSingleton();
            var tileGenerator = TinyIoC.TinyIoCContainer.Current.Resolve<TileGenerator>();
            tileGenerator.Setup(string.Empty, "roads.shp");
        }

        /// <summary>
        /// Tests1.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestMethod1()
        {
            var tileGenerator = TinyIoC.TinyIoCContainer.Current.Resolve<TileGenerator>();
            var tile = await tileGenerator.GetTile(7910, 5314, 14);
            Assert.IsNotNull(tile);
        }
    }
}