using System.Threading;

using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;

using Compass.Common.IO.ShapeFile;
using Compass.Tessera.Core;
using Compass.Tessera.Core.Interface;
using Compass.Tessera.IO;

namespace SampleAndroidApplication
{
    /// <summary>
    /// An activity displaying a full screen map
    /// </summary>
    [Activity(Label = "SampleAndroidApplication", MainLauncher = true, Icon = "@drawable/icon")]
    public class MapActivity : Activity
    {
        private TileOverlayOptions tileOverlayOptions;
        private TileProvider tileProvider;
        // ReSharper disable once NotAccessedField.Local
        private TileOverlay tileOverlay;

        private MapFragment mapFragment;

        /// <inheritdoc />
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map);
            var basePath = Environment.ExternalStorageDirectory.AbsolutePath;

            TinyIoC.TinyIoCContainer.Current.Register<ICacheManager>(new CacheManager());
            var cacheManager = TinyIoC.TinyIoCContainer.Current.Resolve<CacheManager>();
            cacheManager.BasePath = basePath;
            TinyIoC.TinyIoCContainer.Current.Register<ITileSystem>(new TileSystem());
            TinyIoC.TinyIoCContainer.Current.Register<IRenderer>(new Renderer());
            TinyIoC.TinyIoCContainer.Current.Register<IShapeFileManager>(new ShapeFileManager());
            TinyIoC.TinyIoCContainer.Current.Register<TileGenerator>().AsSingleton();
            var tileGenerator = TinyIoC.TinyIoCContainer.Current.Resolve<TileGenerator>();

            while (mapFragment.Map == null)
            {
                Thread.Sleep(100);
            }

            var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(new LatLng(53.297990, -6.179508), 15);
            mapFragment.Map.MoveCamera(cameraUpdate);

            tileGenerator.Setup(basePath, "roads.shp");

            tileOverlayOptions = new TileOverlayOptions();
            tileProvider = new TileProvider();
            tileOverlayOptions.Visible(true);
            tileOverlay = mapFragment.Map.AddTileOverlay(tileOverlayOptions.InvokeTileProvider(tileProvider));
        }
    }
}
