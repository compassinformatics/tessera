// <copyright file="Tile.cs" company="Compass Informatics Ltd.">
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
// <summary>A simple tile. For an introduciton to tile system go to
// http://wiki.openstreetmap.org/wiki/Slippy_map_tilenames#X_and_Y.
// </summary>

using GeoAPI.Geometries;

using NetTopologySuite.Geometries;

namespace Compass.Tessera.Core
{
    /// <summary>
    /// A simple tile representation.
    /// </summary>
    public struct Tile
    {
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the zoom level.
        /// </summary>
        /// <value>
        /// The zoom level.
        /// </value>
        public int ZoomLevel { get; set; }

        /// <summary>
        /// Gets or sets the top left coordinate.
        /// </summary>
        /// <value>
        /// The top left.
        /// </value>
        public Point TopLeft { get; set; }

        /// <summary>
        /// Gets the top right coordinate.
        /// </summary>
        /// <value>
        /// The top right.
        /// </value>
        public Point TopRight
        {
            get
            {
                return new Point(BottomRight.X, TopLeft.Y);
            }
        }

        /// <summary>
        /// Gets or sets the bottom right coordinate.
        /// </summary>
        /// <value>
        /// The bottom right.
        /// </value>
        public Point BottomRight { get; set; }

        /// <summary>
        /// Gets or sets the tile width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the meters per pixel. This is the tile resolution
        /// </summary>
        /// <value>
        /// The meters per pixel.
        /// </value>
        public double MetersPerPixel { get; set; }

        /// <summary>
        /// Gets or sets the border. This geometry is used to clip the visible roads
        /// </summary>
        /// <value>
        /// The border.
        /// </value>
        public IPolygon Border { get; set; }
    }
}