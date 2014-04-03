// <copyright file="CacheManager.cs" company="Compass Informatics Ltd.">
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
// <summary>Contains an implementatio of the ICacheManager interface for .Net 4.5.</summary>

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

using Compass.Tessera.Core.Interface;

namespace Compass.Tessera.IO
{
    /// <summary>
    /// Contains an implementatio of the ICacheManager interface for .Net 4.5.
    /// </summary>
    public class CacheManager : ICacheManager
    {
        /// <inheritdoc />
        public string BasePath { get; set; }

        /// <inheritdoc />
        public void Setup(string basePath)
        {
            BasePath = basePath;
        }

        /// <inheritdoc />
        public async Task<MemoryStream> GetTile(string shapefileId, int x, int y, int level)
        {
            var filePath = Path.Combine(BasePath, shapefileId, level.ToString(CultureInfo.InvariantCulture), string.Format("{0}_{1}.png", x, y));
            if (!File.Exists(filePath)) return null;
            try
            {
                using (var fileStream = File.OpenRead(filePath))
                {
                    var tile = new MemoryStream();
                    tile.SetLength(fileStream.Length);
                    await fileStream.ReadAsync(tile.GetBuffer(), 0, (int)fileStream.Length);
                    fileStream.Close();
                    return tile;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <inheritdoc />
        public void SaveTile(string shapefileId, int x, int y, int level, MemoryStream stream)
        {
            var shapeFilePath = Path.Combine(BasePath, shapefileId);
            if (!Directory.Exists(shapeFilePath))
            {
                Directory.CreateDirectory(shapeFilePath);
            }
            var levelPath = Path.Combine(shapeFilePath, level.ToString(CultureInfo.InvariantCulture));
            if (!Directory.Exists(levelPath))
            {
                Directory.CreateDirectory(levelPath);
            }
            var filePath = Path.Combine(levelPath, string.Format("{0}_{1}.png", x, y));
            if (File.Exists(filePath)) return;
            var bitmap = new Bitmap(stream);
            new Task(() => bitmap.Save(filePath, ImageFormat.Png)).Start();
            stream.Position = 0;
        }
    }
}
