using Android.Gms.Maps.Model;

using Compass.Tessera.Core;

using TinyIoC;

using Tile = Android.Gms.Maps.Model.Tile;

namespace SampleAndroidApplication
{
    /// <inheritdoc />
    public class TileProvider : Java.Lang.Object, ITileProvider
    {
        private readonly TileGenerator tileGenerator;

        /// <inheritdoc />
        public TileProvider()
        {
            tileGenerator = TinyIoCContainer.Current.Resolve<TileGenerator>();
        }

        /// <inheritdoc />
        public Tile GetTile(int x, int y, int zoom)
        {
            var byteArray = tileGenerator.GetTile(x, y, zoom);
            if (byteArray.Result == null) return Android.Gms.Maps.Model.TileProvider.NoTile;
            var tile = new Tile(256, 256, byteArray.Result.ToArray());
            return tile;
        }
    }
}