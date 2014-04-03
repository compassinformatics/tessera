// <copyright file="PointF.cs" company="Compass Informatics Ltd.">
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
// <summary>Contains a simple Point with floating coordinates.</summary>

using System.Globalization;

namespace Compass.Tessera.Core
{
    /// <summary>
    /// A simple Point with floating coordinates.
    /// </summary>
    public struct PointF
    {
        private readonly float x;
        private readonly float y;

        /// <summary>
        /// Gets the x coordinate.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        public float X
        {
            get
            {
                return x;
            }
        }

        /// <summary>
        /// Gets the y coordinate.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        public float Y
        {
            get
            {
                return y;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointF"/> struct.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public PointF(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Implementation of the == operator
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>Returns true if left and right are equal</returns>
        public static bool operator ==(PointF left, PointF right)
        {
            if (left.X == (double)right.X)
                return left.Y == (double)right.Y;
            return false;
        }

        /// <summary>
        /// Implementation of the != operator
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>Returns true if left and right are different</returns>
        public static bool operator !=(PointF left, PointF right)
        {
            if (left.X == (double)right.X)
                return left.Y != (double)right.Y;
            return true;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is PointF))
                return false;
            return this == (PointF)obj;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (int)x ^ (int)y;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format("{{X={0}, Y={1}}}", (object)x.ToString(CultureInfo.CurrentCulture), (object)y.ToString(CultureInfo.CurrentCulture));
        }
    }
}