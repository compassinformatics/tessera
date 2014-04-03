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
// <summary>Contains an implementatio of the IRenderer interface for Xamarin.Android.</summary>

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Android.Graphics;

using Compass.Tessera.Core.Interface;

namespace Compass.Tessera.IO
{
    /// <summary>
    /// Contains an implementatio of the IRenderer interface for Xamarin.Android.
    /// </summary>
    public class Renderer : IRenderer
    {
        private static readonly Paint Paint = new Paint { AntiAlias = true, Color = Color.Red, StrokeWidth = 2, StrokeJoin = Paint.Join.Round };

        /// <inheritdoc />
        public MemoryStream RenderTileToMemory(IEnumerable<IList<Core.PointF>> items)
        {
            var stream = new MemoryStream();
            var bitmap = Bitmap.CreateBitmap(256, 256, Bitmap.Config.Argb4444);

            using (var canvas = new Canvas(bitmap))
            {
                var points = new Collection<float>();
                foreach (var mappedPoints in items.Where(mappedPoints => mappedPoints.Count() >= 2))
                {
                    for (var i = 1; i < mappedPoints.Count; i++)
                    {
                        points.Add(mappedPoints[i - 1].X);
                        points.Add(mappedPoints[i - 1].Y);
                        points.Add(mappedPoints[i].X);
                        points.Add(mappedPoints[i].Y);
                    }
                }
                canvas.DrawLines(points.ToArray(), Paint);
            }
            bitmap.Compress(Bitmap.CompressFormat.Png, 9, stream);
            bitmap.Dispose();
            stream.Position = 0;
            return stream;
        }
    }
}