// <copyright file="IRenderer.cs" company="Compass Informatics Ltd.">
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
// <summary>Contains the IRendered interface.</summary>

using System.Collections.Generic;
using System.IO;

namespace Compass.Tessera.Core.Interface
{
    /// <summary>
    /// Provides the rendering functionality for Compass Tessera.
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// Renders the tile to memory.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        MemoryStream RenderTileToMemory(IEnumerable<IList<PointF>> items);
    }
}