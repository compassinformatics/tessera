// <copyright file="Renderer.cs" company="Compass Informatics Ltd.">
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
// <summary>Contains an implementatio of the IRenderer interface for .Net 4.5.</summary>

using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

using Compass.Tessera.Core.Interface;

using PointF = Compass.Tessera.Core.PointF;

namespace Compass.Tessera.IO
{
    /// <summary>
    /// Contains an implementatio of the IRenderer interface for .Net 4.5.
    /// </summary>
    public class Renderer : IRenderer
    {
        /// <inheritdoc />
        public MemoryStream RenderTileToMemory(IEnumerable<IList<PointF>> items)
        {
            var internalItems = new List<List<System.Drawing.PointF>>();
            foreach (var item in items)
            {
                var tempList = new List<System.Drawing.PointF>();
                foreach (var pointF in item)
                {
                    tempList.Add(new System.Drawing.PointF(pointF.X, pointF.Y));
                }
                internalItems.Add(tempList);
            }
            var stream = new MemoryStream();
            var bitmap = new Bitmap(256, 256);
            bitmap.MakeTransparent();
            var pen = new Pen(Color.Red, 2);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                foreach (var mappedPoints in internalItems.Where(mappedPoints => mappedPoints.Count() >= 2))
                {
                    g.DrawLines(pen, mappedPoints.ToArray());
                }
            }
            bitmap.Save(stream, ImageFormat.Png);
            stream.Position = 0;
            return stream;
        }
    }
}