// <copyright file="ITileSystem.cs" company="Compass Informatics Ltd.">
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
// <summary>Contains the ITileSystem interface.</summary>

using GeoAPI.Geometries;

using NetTopologySuite.Geometries;

namespace Compass.Tessera.Core.Interface
{
    /// <summary>
    /// Defines the basic functions to implement a tile system
    /// </summary>
    public interface ITileSystem
    {
        /// <summary>
        /// Gets or sets the size of the tile.
        /// </summary>
        /// <value>
        /// The size of the tile.
        /// </value>
        int TileSize { get; set; }

        /// <summary>
        /// Used to create a Tile based on position and zoom level
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="zoom">The zoom.</param>
        /// <returns>Returns a Tile</returns>
        Tile TileFactory(int x, int y, int zoom);

        /// <summary>
        /// Returns the coordinates of the top left corner of a tile
        /// </summary>
        /// <param name="tileX">The tile x.</param>
        /// <param name="tileY">The tile y.</param>
        /// <param name="zoom">The zoom.</param>
        /// <returns></returns>
        Point TileToWorld(int tileX, int tileY, int zoom);

        /// <summary>
        /// Returns the tile position based on latitude and longitude.
        /// </summary>
        /// <param name="lon">The lon.</param>
        /// <param name="lat">The lat.</param>
        /// <param name="zoom">The zoom.</param>
        /// <returns></returns>
        int[] WorldToTile(double lon, double lat, int zoom);

        /// <summary>
        /// Converts a coordinate to the tile image position 
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="tile">The tile.</param>
        /// <returns></returns>
        Point WorldToMapPixel(IPoint point, Tile tile);
    }
}