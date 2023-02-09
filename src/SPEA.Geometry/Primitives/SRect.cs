// ==================================================================================================
// <copyright file="SRect.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Primitives
{
    using SPEA.Geometry.Core;

    /// <summary>
    /// Represents a rectangle <see cref="SObject"/> primitive.
    /// </summary>
    public sealed class SRect : SPrimitive
    {
        #region Fields

        private double _width;
        private double _height;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SRect"/> class.
        /// </summary>
        /// <param name="x">The X-coordinate of the origin.</param>
        /// <param name="y">The Y-coordinate of the origin.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when any of the parameters is out of its valid range.</exception>
        public SRect(double x, double y, double width, double height)
            : base(x, y)
        {
            if (double.IsNaN(x) || double.IsInfinity(x))
            {
                throw new ArgumentOutOfRangeException(nameof(x), "The origin coordinates must be finite and valid.");
            }

            if (double.IsNaN(y) || double.IsInfinity(y))
            {
                throw new ArgumentOutOfRangeException(nameof(y), "The origin coordinates must be finite and valid.");
            }

            if (Math.Abs(width) == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Width cannot be zero.");
            }

            if (Math.Abs(height) == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height), "Height cannot be zero.");
            }

            Width = width;
            Height = height;
            Geometry = BuildGeometry(width, height);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SRect"/> class.
        /// </summary>
        /// <param name="origin">The origin location.</param>
        /// <param name="point">The location of the point located in the corner opposite to the origin.</param>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when any of the parameters is out of its valid range.</exception>
        public SRect(SPoint origin, SPoint point)
            : base(origin)
        {
            var width = point.X - origin.X;
            var height = point.Y - origin.Y;

            if (Math.Abs(width) == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Width cannot be zero.");
            }

            if (Math.Abs(height) == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height), "Height cannot be zero.");
            }

            Width = width;
            Height = height;
            Geometry = BuildGeometry(width, height);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SRect"/> class.
        /// </summary>
        /// <param name="origin">The origin location.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        public SRect(SPoint origin, double width, double height)
            : this(origin.X, origin.Y, width, height)
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SRect"/> class.
        /// </summary>
        /// <param name="x">The X-coordinate of the origin.</param>
        /// <param name="y">The Y-coordinate of the origin.</param>
        /// <param name="point">The location of the point located in the corner opposite to the origin.</param>
        public SRect(double x, double y, SPoint point)
            : this(x, y, point.X, point.Y)
        {
            // Blank.
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the rectangle width.
        /// </summary>
        public double Width
        {
            get => _width;
            set => _width = value;
        }

        /// <summary>
        /// Gets or sets the rectangle height.
        /// </summary>
        public double Height
        {
            get => _height;
            set => _height = value;
        }

        #endregion Properties

        #region Methods

        // Generate a rectangle geometry using its width and height.
        // Values are set in the element's local coordinate system, relative to its origin point.
        // Points are added from the bottom-left corner in the counter-clockwise direction.
        private static SGeometry BuildGeometry(double width, double height)
        {
            var p0 = new SPoint(0, 0);
            var p1 = new SPoint(0 + width, 0);
            var p2 = new SPoint(0 + width, 0 + height);
            var p3 = new SPoint(0, 0 + height);

            return new SGeometry(p0, p1, p2, p3);
        }

        /////// <inheritdoc/>
        ////protected override void BuildGeometry()
        ////{
        ////    throw new NotImplementedException();
        ////}

        #endregion Methods
    }
}
