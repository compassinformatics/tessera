// <copyright file="TileSystem.cs" company="Compass Informatics Ltd.">
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
// <summary>An implementation of the ITileSystem interface that represents the Google Tile System.</summary>

using System;

using Compass.Tessera.Core.Interface;

using GeoAPI.Geometries;

using NetTopologySuite.Geometries;

namespace Compass.Tessera.Core
{
    /// <summary>
    /// An implementation of the ITileSystem interface that represents the Google Tile System.
    /// </summary>
    public class TileSystem : ITileSystem
    {
        private int tileSize = 256;

        /// <summary>
        /// Gets or sets the size of the tile.
        /// </summary>
        /// <value>
        /// The size of the tile.
        /// </value>
        public int TileSize
        {
            get
            {
                return tileSize;
            }

            set
            {
                tileSize = value;
            }
        }

        /// <summary>
        /// Tiles the factory.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="zoom">The zoom.</param>
        /// <returns></returns>
        public Tile TileFactory(int x, int y, int zoom)
        {
            var tile = new Tile
                           {
                               X = x,
                               Y = y,
                               ZoomLevel = zoom,
                               TopLeft = TileToWorld(x, y, zoom),
                               BottomRight = TileToWorld(x + 1, y + 1, zoom)
                           };
            tile.Border = new GeometryFactory().CreatePolygon(new[] 
                                                                    {
                                                                        new Coordinate { X = tile.TopLeft.X, Y = tile.TopLeft.Y },
                                                                        new Coordinate { X = tile.TopLeft.X, Y = tile.BottomRight.Y },
                                                                        new Coordinate { X = tile.BottomRight.X, Y = tile.BottomRight.Y },
                                                                        new Coordinate { X = tile.BottomRight.X, Y = tile.TopLeft.Y },
                                                                        new Coordinate { X = tile.TopLeft.X, Y = tile.TopLeft.Y } 
                                                                    });

            tile.Width = Math.Abs(tile.TopLeft.X - tile.TopRight.X);
            tile.MetersPerPixel = tile.Width / TileSize;
            return tile;
        }

        /// <summary>
        /// Tiles to world.
        /// </summary>
        /// <param name="tileX">The tile x.</param>
        /// <param name="tileY">The tile y.</param>
        /// <param name="zoom">The zoom.</param>
        /// <returns></returns>
        public Point TileToWorld(int tileX, int tileY, int zoom)
        {
            var n = Math.PI - ((2.0 * Math.PI * tileY) / Math.Pow(2.0, zoom));
            var x = (float)((tileX / Math.Pow(2.0, zoom) * 360.0) - 180.0);
            var y = (float)(180.0 / Math.PI * Math.Atan(Math.Sinh(n)));
            var wm = Wgs84ToMercator(x, y);
            return new Point(wm[0], wm[1]);
        }

        /// <summary>
        /// Worlds to tile.
        /// </summary>
        /// <param name="lon">The lon.</param>
        /// <param name="lat">The lat.</param>
        /// <param name="zoom">The zoom.</param>
        /// <returns></returns>
        public int[] WorldToTile(double lon, double lat, int zoom)
        {
            var x = (float)((lon + 180.0) / 360.0 * (1 << zoom));
            var y = (float)((1.0 - Math.Log(Math.Tan((lat * Math.PI) / 180.0) +
                1.0 / Math.Cos(lat * Math.PI / 180.0)) / Math.PI) / 2.0 * (1 << zoom));
            return new[] { (int)x, (int)y };
        }

        /// <summary>
        /// Worlds to map pixel.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="tile">The tile.</param>
        /// <returns></returns>
        public Point WorldToMapPixel(IPoint point, Tile tile)
        {
            IPoint x = new Point(tile.TopLeft.X, point.Y);
            IPoint y = new Point(point.X, tile.TopLeft.Y);
            var dx = Math.Abs(point.X - x.X);
            var dy = Math.Abs(point.Y - y.Y);
            var r = new Point((int)(dx / tile.MetersPerPixel), (int)(dy / tile.MetersPerPixel));
            return r;
        }

        private double[] Wgs84ToMercator(double lon, double lat)
        {
            var x = lon * 20037508.34 / 180;
            var y = Math.Log(Math.Tan((90 + lat) * Math.PI / 360)) / (Math.PI / 180);
            y = y * 20037508.34 / 180;
            return new[] { x, y };
        }
    }
}