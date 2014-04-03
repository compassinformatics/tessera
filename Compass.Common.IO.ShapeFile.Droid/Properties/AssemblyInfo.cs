using System.Reflection;
using System.Runtime.InteropServices;
using Android.App;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Compass.Common.IO.ShapeFile.Droid")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Compass Informatics Ltd")]
[assembly: AssemblyProduct("Compass.Common.IO.ShapeFile.Droid")]
[assembly: AssemblyCopyright("Copyright © Compass Informatics 2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]

// Version information for an assembly consists of the following values:
//
// Given a version number MAJOR.MINOR.PATCH, increment the:
// 1.MAJOR version when you make incompatible API changes,
// 2.MINOR version when you add functionality in a backwards-compatible manner, and
// 3.PATCH version when you make backwards-compatible bug fixes.
// Additional labels for pre-release and build metadata are available as extensions to the MAJOR.MINOR.PATCH format.
// See http://semver.org/   

[assembly: AssemblyVersion("0.1.0")]
[assembly: AssemblyFileVersion("0.1.0")]

// Add some common permissions, these can be removed if not needed
[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
[assembly: UsesPermission(Android.Manifest.Permission.WriteExternalStorage)]
