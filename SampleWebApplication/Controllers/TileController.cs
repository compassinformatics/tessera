using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

using Compass.Tessera.Core;

namespace SampleWebApplication.Controllers
{
    /// <summary>
    /// The WebAPI Tile Controller
    /// </summary>
    public class TileController : ApiController
    {
        private TileGenerator tileGenerator;

        private TileGenerator TileGenerator
        {
            get { return tileGenerator ?? (tileGenerator = TinyIoC.TinyIoCContainer.Current.Resolve<TileGenerator>()); }
        }

        /// <summary>
        /// Gets the image representing the requested tile.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="zoomLevel">The zoom level.</param>
        /// <returns>Returns a PNG image</returns>
        public async Task<HttpResponseMessage> GetTile(int x, int y, int zoomLevel)
        {
            var ms = await TileGenerator.GetTile(x, y, zoomLevel);
            if (ms != null)
            {
                var response = new HttpResponseMessage { Content = new StreamContent(ms) };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                return response;    
            }
            return null;
        }
    }
}
