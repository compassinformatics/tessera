// <copyright file="TileGenerator.cs" company="Compass Informatics Ltd.">
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
// <summary>A class that generates map tiles based on the tile position and zoom level.</summary>

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Compass.Tessera.Core.Interface;

using NetTopologySuite.Geometries;

namespace Compass.Tessera.Core
{
    /// <summary>
    /// A class that generates map tiles based on the tile position and zoom level.
    /// </summary>
    public class TileGenerator
    {
        private string shapefileId;

        private readonly IRenderer renderer;
        private readonly ICacheManager cacheManager;
        private readonly IShapeFileManager shapeFileManager;
        private readonly ITileSystem tileSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="TileGenerator" /> class.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="shapeFileManager">The shape file manager.</param>
        /// <param name="tileSystem">The tile system.</param>
        public TileGenerator(IRenderer renderer, ICacheManager cacheManager, IShapeFileManager shapeFileManager, ITileSystem tileSystem)
        {
            this.renderer = renderer;
            this.cacheManager = cacheManager;
            this.shapeFileManager = shapeFileManager;
            this.tileSystem = tileSystem;
        }

        /// <summary>
        /// Setups the specified base path.
        /// </summary>
        /// <param name="shapeFile">The shape file.</param>
        public void Setup(string shapeFile)
        {
            shapefileId = shapeFile.TrimEnd(".shp".ToCharArray());
        }

        /// <summary>
        /// Setups the specified base path.
        /// </summary>
        /// <param name="basePath">The base path.</param>
        /// <param name="shapeFile">The shape file.</param>
        public void Setup(string basePath, string shapeFile)
        {
            shapefileId = shapeFile.TrimEnd(".shp".ToCharArray());
            cacheManager.BasePath = basePath;
            shapeFileManager.LoadShapeFile(Path.Combine(basePath, shapeFile));
        }

        /// <summary>
        /// Gets the tile.
        /// </summary>
        /// <param name="x">The executable.</param>
        /// <param name="y">The asynchronous.</param>
        /// <param name="zoomLevel">The zoom level.</param>
        /// <returns></returns>
        public async Task<MemoryStream> GetTile(int x, int y, int zoomLevel)
        {
            var stream = await cacheManager.GetTile(shapefileId, x, y, zoomLevel);

            if (stream != null)
            {
                return stream;
            }

            var tile = tileSystem.TileFactory(x, y, zoomLevel);
            var clippedLines = ClipShapeFile(tile).ToList();

            if (clippedLines == null || clippedLines.All(m => m == LineString.Empty))
            {
                return null;
            }
            var tileMappedLines = GetMappedPoints(clippedLines, tile);
            stream = renderer.RenderTileToMemory(tileMappedLines);
            cacheManager.SaveTile(shapefileId, x, y, zoomLevel, stream);
            return stream;
        }

        //public void GenerateTiles()
        //{
        //    for (var i = 1; i < 20; i++)
        //    {
        //        var totalTiles = Math.Pow(i, 2) * Math.Pow(i, 2);
        //        var topLeft = CoordinateConverter.Wm2Wgs84(new Point(shapeFileManager.Bounds.MinX, shapeFileManager.Bounds.MaxY));
        //        var bottomRight = CoordinateConverter.Wm2Wgs84(new Point(shapeFileManager.Bounds.MaxX, shapeFileManager.Bounds.MinY));

        //        var startTile = TileSystem.WorldToTile(topLeft.X, topLeft.Y, i);
        //        var endTile = TileSystem.WorldToTile(bottomRight.X, bottomRight.Y, i);
        //        for (var x = startTile[0]; x <= endTile[0]; x++)
        //        {
        //            for (var y = startTile[1]; y <= endTile[1]; y++)
        //            {
        //                GetTile(x, y, i);
        //            }
        //        }
        //    }
        //}

        private IEnumerable<IList<PointF>> GetMappedPoints(IEnumerable<LineString> clippedLines, Tile tile)
        {
            var listItems = new Collection<Collection<PointF>>();
            foreach (var clippedLine in clippedLines)
            {
                if (clippedLine == null) continue;
                var mappedPoints = new Collection<PointF>();
                for (var i = 0; i < clippedLine.NumPoints; i++)
                {
                    var mappedPoint = tileSystem.WorldToMapPixel(clippedLine.GetPointN(i), tile);
                    mappedPoints.Add(new PointF((float)mappedPoint.X, (float)mappedPoint.Y));
                }
                listItems.Add(mappedPoints);
            }
            return listItems;
        }

        private IEnumerable<LineString> ClipShapeFile(Tile tile)
        {
            var geometries = shapeFileManager.IndexTree.Query(tile.Border.EnvelopeInternal);

            if (geometries.Count == 0)
                return null;
            var clippedLines = new List<LineString>();

            foreach (var geometry in geometries.Where(m => m != null))
            {
                var clip = tile.Border.Intersection(geometry);
                var s = clip as MultiLineString;
                if (s != null)
                {
                    clippedLines.AddRange(s.Select(t => t as LineString));
                }
                else if (clip is LineString)
                {
                    clippedLines.Add(clip as LineString);
                }
            }
            return clippedLines;
        }
    }
}