// <copyright file="ICacheManager.cs" company="Compass Informatics Ltd.">
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
// </copyright>
// <author>Michele Scandura</author>
// <email>mscandura@compass.ie</email>
// <date>02-04-2014</date>
// <summary>Contains the ICacheManager interface.</summary>

using System.IO;
using System.Threading.Tasks;

namespace Compass.Tessera.Core.Interface
{
    /// <summary>
    /// Provides the caching functionality for Compass Tessera.
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Returns the image for the specified shapefile based on the tile location and zoom level.
        /// </summary>
        /// <param name="shapeFileId">The shape file identifier.</param>
        /// <param name="x">The x tile.</param>
        /// <param name="y">The y tile.</param>
        /// <param name="zoomLevel">The zoom level.</param>
        /// <returns>Return a MemoryStream containing the image</returns>
        Task<MemoryStream> GetTile(string shapeFileId, int x, int y, int zoomLevel);

        /// <summary>
        /// Saves the generated tile to the local storage. Storage could be anything.
        /// </summary>
        /// <param name="shapeFileId">The shape file identifier.</param>
        /// <param name="x">The x tile.</param>
        /// <param name="y">The y tile.</param>
        /// <param name="zoomLevel">The zoom level.</param>
        /// <param name="stream">The stream.</param>
        void SaveTile(string shapeFileId, int x, int y, int zoomLevel, MemoryStream stream);

        /// <summary>
        /// Gets or sets the base path containing the shapefile
        /// </summary>
        /// <value>
        /// The base path.
        /// </value>
        string BasePath { get; set; }

        /// <summary>
        /// Performs the setup of the Cache Manager
        /// </summary>
        /// <param name="basePath">The base path.</param>
        void Setup(string basePath);
    }
}