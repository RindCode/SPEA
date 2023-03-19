// ==================================================================================================
// <copyright file="BoundingBox.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

using SPEA.Geometry.Core;

namespace SPEA.Geometry.Misc
{
    /// <summary>
    /// Represents a bounding box of an <see cref="Core.SLinearRing"/> object.
    /// </summary>
    public readonly struct BoundingBox
    {
        #region Fields

        private readonly double _left;
        private readonly double _top;
        private readonly double _right;
        private readonly double _bottom;
        private readonly SPoint _center;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingBox"/> struct.
        /// </summary>
        public BoundingBox()
            : this(0, 0, 0, 0)
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingBox"/> struct.
        /// </summary>
        /// <param name="left">The X-component of the left border.</param>
        /// <param name="top">The Y-component of the top border.</param>
        /// <param name="right">The X-component of the right border.</param>
        /// <param name="bottom">The Y-component of the bottom border.</param>
        public BoundingBox(double left, double top, double right, double bottom)
        {
            if (double.IsNaN(left) || double.IsInfinity(left))
            {
                throw new ArgumentOutOfRangeException(nameof(left), "The coordinates must be finite and valid.");
            }

            if (double.IsNaN(top) || double.IsInfinity(top))
            {
                throw new ArgumentOutOfRangeException(nameof(top), "The coordinates must be finite and valid.");
            }

            if (double.IsNaN(right) || double.IsInfinity(right))
            {
                throw new ArgumentOutOfRangeException(nameof(right), "The coordinates must be finite and valid.");
            }

            if (double.IsNaN(bottom) || double.IsInfinity(bottom))
            {
                throw new ArgumentOutOfRangeException(nameof(bottom), "The coordinates must be finite and valid.");
            }

            _left = left;
            _top = top;
            _right = right;
            _bottom = bottom;
            _center = new SPoint((left + right) / 2, (top + bottom) / 2);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the X-component of the left border.
        /// </summary>
        public double Left => _left;

        /// <summary>
        /// Gets the Y-component of the top border.
        /// </summary>
        public double Top => _top;

        /// <summary>
        /// Gets the X-component of the right border.
        /// </summary>
        public double Right => _right;

        /// <summary>
        /// Gets the Y-component of the bottom border.
        /// </summary>
        public double Bottom => _bottom;

        /// <summary>
        /// Gets the bounding box center.
        /// </summary>
        public SPoint Center => _center;

        #endregion Properties
    }
}
