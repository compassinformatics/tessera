// <copyright file="ShapeFileManager.cs" company="Compass Informatics Ltd.">
// Copyright (c) Compass Informatics 2014, All Right Reserved, http://compass.ie/
//
// This source is subject to the MIT License.
// Please see the License file for more information.
// All other rights reserved.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// </copyright>
// <author>Michele Scandura</author>
// <email>mscandura@compass.ie</email>
// <date>02-04-2014</date>
// <summary>Contains an implementatio of the IShapeFileManager.</summary>

using System;

using Android.Content.Res;

using Compass.Tessera.Core.Interface;

using GeoAPI.Geometries;

using NetTopologySuite.CoordinateSystems.Transformations;
using NetTopologySuite.Geometries;
using NetTopologySuite.Index.Strtree;
using NetTopologySuite.IO;

using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

namespace Compass.Common.IO.ShapeFile
{
    /// <summary>
    /// Contains an implementatio of the IShapeFileManager.
    /// </summary>
    public class ShapeFileManager : IShapeFileManager
    {
        /// <summary>
        /// Gets or sets the shapefile index.
        /// </summary>
        public STRtree<Geometry> IndexTree { get; set; }

        /// <summary>
        /// Gets or sets the bounds.
        /// </summary>
        /// <value>
        /// The bounds.
        /// </value>
        public Envelope Bounds { get; set; }

        /// <summary>
        /// Loads the shape file.
        /// </summary>
        /// <param name="shapeFileName">Name of the shape file.</param>
        public void LoadShapeFile(string shapeFileName)
        {
            IndexTree = new STRtree<Geometry>();
            var factory = new GeometryFactory();

            var shapeFileDataReader = new ShapefileDataReader(shapeFileName, factory);
            
            var precisionModel = new PrecisionModel(PrecisionModels.Floating);
            var wgs84 = GeographicCoordinateSystem.WGS84;
            var webMercator = ProjectedCoordinateSystem.WebMercator;
            var sridWebmercator = Convert.ToInt32(webMercator.AuthorityCode);
            var coordinateTransformationFactory = new CoordinateTransformationFactory();
            var transformation = coordinateTransformationFactory.CreateFromCoordinateSystems(wgs84, webMercator);
            var factoryWebmercator = new GeometryFactory(precisionModel, sridWebmercator);

            var shpHeader = shapeFileDataReader.ShapeHeader;

            //Display the min and max bounds of the shapefile
            Bounds = GeometryTransform.TransformBox(shpHeader.Bounds, transformation.MathTransform);
            //Reset the pointer to the start of the shapefile, just in case
            shapeFileDataReader.Reset();
            
            while (shapeFileDataReader.Read())
            {
                var geometry = (Geometry)GeometryTransform.TransformGeometry(factoryWebmercator, shapeFileDataReader.Geometry, transformation.MathTransform);
                var env = geometry.EnvelopeInternal;
                IndexTree.Insert(env, geometry);
            } //Close and free up any resourcesshapeFileDataReader.Close();

            shapeFileDataReader.Close();
            shapeFileDataReader.Dispose();
        }
    }
}