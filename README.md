[![] (http://www.compass.ie/wordpress/wp-content/uploads/Compass-logo-Final-300x104.png)](http://www.compass.ie)

# Compass Tessera

[**Visit the demo website.**](http://w08-iis.compass.ie/tesserademo/)  

Compass Tessera (as in [Tessera](http://en.wikipedia.org/wiki/Tessera)) is a simple shapefile tile engine written in C#.
The purpose of the library is to provide an easy way to render a shapefile into tiles that can be used as an overlay on Google Maps or similar services.
It's currently being used by 30 Local Authorities in Ireland as part of the [PMS project](http://www.compass.ie/pavement-management-system/) to display the official road network on an Android application operating offline.

## Usage example

```
TinyIoC.TinyIoCContainer.Current.AutoRegister();
TinyIoC.TinyIoCContainer.Current.Register<TileGenerator>().AsSingleton();
var tileGenerator = TinyIoC.TinyIoCContainer.Current.Resolve<TileGenerator>();
tileGenerator.Setup(HostingEnvironment.MapPath("~/"), "roads.shp");
```
and then 

```
var memoryStream = tileGenerator.GetTile(7910,5314,14);
```
## Supported Platforms

* .NET 4.5 (Desktop / Server)
* Xamarin.Android (probably also Xamarin.iOS and Xamarin.Mac)

The application relies on dependency injection to support multiple platforms. The IoC container used in the sample applications is [TinyIoC](https://github.com/grumpydev/TinyIoC).


## Build

Compass Tessera is built using

* Visual Studio 2013 or Xamarin Studio
* The Xamarin framework

## Sample Applications


This project contains two sample applications:

1. An MVC web application
2. A Xamarin.Android app

**NOTE** You will have to generate a Google Maps API key for both applications in order to use the Google Maps control.

### MVC web application

The application provides a single page with a Google Map set up with a custom tile overlay.
A WebAPI web service is used to serve the tiles with a simple url scheme

````
http://localhost/api/tile/gettile?x=7910&y=5314&zoomLevel=14
````

### Xamarin.Android App
The app  displays a full screen Google Maps map (Google Maps Api V2) and it's set up to generate the tiles on the device.
The big advantage is that the background mapping is not necessary to display the tiles, making the tile generation fully working offline.
After deploying the app to the device, copy the the roads.* files in the Maps folder of the web application into the root folder of the device.


## Known bugs and limitations
- The library currently supports only WGS84 -EPSG:4326  as input shapefiles.
- The style of the lines is hardcoded
- There is a small gap between tiles

## TODO
- Suite of unit tests
- Support of different projections in the input file
- Code cleanup

## Credits
* [NetTopologySuite](http://code.google.com/p/nettopologysuite)
* [GeoFabrik](http://download.geofabrik.de/) for the Ireland road map shapefile used in the sample applications.

## Libraries
- The application is using the portable version of the  [NetTopologySuite](http://code.google.com/p/nettopologysuite) library and its dependencies. For convenience the compiled assemblies are available in this repository.
- [Proj.Net](https://projnet.codeplex.com/)
- [GeoAPI.NET](http://geoapi.codeplex.com/)
- [TinyIoC](https://github.com/grumpydev/TinyIoC)


## Feedback
All bugs, feature requests, pull requests, feedback, etc., are welcome. [Create an issue](https://github.com/compassinformatics/tessera/issues). 
Or ask a question on [StackOverflow](http://stackoverflow.com/questions/tagged/compass-tessera)

## License
Copyright 2014, [Compass Informatics Ltd](http://www.compass.ie/).

Licensed under the [MIT License](http://opensource.org/licenses/MIT) or see the [`LICENSE`](https://github.com/compassinformatics/tessera/blob/master/LICENSE) file.

## Author
- Michele Scandura - 
[gitHub](https://github.com/mikescandy) or  [Twitter](https://twitter.com/mikescandy)
- Compass Informatics - [gitHub](https://github.com/compassinformatics), [Twitter](https://twitter.com/CompassInfo) or [web](https://github.com/mikescandy) 


## Logo
Copyright 2014, [Compass Informatics Ltd](http://www.compass.ie/).

