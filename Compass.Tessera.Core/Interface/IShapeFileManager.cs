// <copyright file="IShapeFileManager.cs" company="Compass Informatics Ltd.">
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
// <summary>Contains the IShapeFileManager interface.</summary>

using NetTopologySuite.Geometries;
using NetTopologySuite.Index.Strtree;

namespace Compass.Tessera.Core.Interface
{
    /// <summary>
    /// Used to manage the basic functionaliies regarding a shapefile
    /// </summary>
    public interface IShapeFileManager
    {
        /// <summary>
        /// Gets or sets the index that will be used to query the shapefile.
        /// </summary>
        /// <value>
        /// The index tree.
        /// </value>
        STRtree<Geometry> IndexTree { get; set; }

        /// <summary>
        /// Gets or sets theshapefile boundaries.
        /// </summary>
        /// <value>
        /// The bounds.
        /// </value>
        GeoAPI.Geometries.Envelope Bounds { get; set; }

        /// <summary>
        /// Loads the shapefile.
        /// </summary>
        /// <param name="shapeFileName">Name of the shapefile.</param>
        void LoadShapeFile(string shapeFileName);
    }
}