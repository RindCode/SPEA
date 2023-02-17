﻿// ==================================================================================================
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
    public sealed class SRect : SPolygonBase
    {
        #region Fields

        private readonly SLinearRing _shell;
        private readonly SLinearRing[] _holes;
        private double _width;
        private double _height;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SRect"/> class.
        /// </summary>
        /// <param name="x">The X-coordinate of the origin.</param>
        /// <param name="y">The Y-coordinate of the origin.</param>
        /// <param name="width">The width of the rectangle. Can be negative.</param>
        /// <param name="height">The height of the rectangle. Can be negative.</param>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when any of the parameters is out of its valid range.</exception>
        public SRect(double x, double y, double width, double height)
        {
            if (double.IsNaN(x) || double.IsInfinity(x))
            {
                throw new ArgumentOutOfRangeException(nameof(x), "The origin coordinates must be finite and valid.");
            }

            if (double.IsNaN(y) || double.IsInfinity(y))
            {
                throw new ArgumentOutOfRangeException(nameof(y), "The origin coordinates must be finite and valid.");
            }

            if (width == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Width cannot be zero.");
            }

            if (height == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height), "Width cannot be zero.");
            }

            _width = width;
            _height = height;
            _holes = Array.Empty<SLinearRing>();

            var geom = BuildGeometry(width, height);
            geom.Translate(x, y);
            _shell = geom;
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
        /// <param name="origin">The origin location.</param>
        /// <param name="point">The location of the point located in the corner opposite to the origin.</param>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when any of the parameters is out of its valid range.</exception>
        public SRect(SPoint origin, SPoint point)
        {
            var width = point.X - origin.X;
            var height = point.Y - origin.Y;

            if (width == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Width cannot be zero.");
            }

            if (height == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height), "Height cannot be zero.");
            }

            _width = width;
            _height = height;
            _holes = Array.Empty<SLinearRing>();

            var geom = BuildGeometry(width, height);
            geom.Translate(origin.X, origin.Y);
            _shell = geom;
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

        /// <inheritdoc/>
        public override SLinearRing Shell => _shell;

        /// <inheritdoc/>
        public override SLinearRing[] Holes => _holes;

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

        // Generates a rectangle geometry using its width and height.
        private static SLinearRing BuildGeometry(double width, double height)
        {
            var p0 = new SPoint(0, 0);
            var p1 = new SPoint(0 + width, 0);
            var p2 = new SPoint(0 + width, 0 + height);
            var p3 = new SPoint(0, 0 + height);

            var points = new SPoint[4] { p0, p1, p2, p3 };
            var geom = new SLinearRing(points);

            return geom;
        }

        #endregion Methods
    }
}
