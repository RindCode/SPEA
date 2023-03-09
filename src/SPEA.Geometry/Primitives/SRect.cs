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
    public sealed class SRect : SPolygon
    {
        #region Fields

        /// <summary>
        /// Gets the internal type of this entity.
        /// </summary>
        public new const EntityType InternalType = EntityType.SRECT;

        private readonly SLinearRing _shell;
        private readonly SLinearRing[] _holes = Array.Empty<SLinearRing>();
        private readonly double _w;
        private readonly double _h;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SRect"/> class.
        /// </summary>
        /// <param name="x">The X-coordinate of the origin.</param>
        /// <param name="y">The Y-coordinate of the origin.</param>
        /// <param name="w">The w of the rectangle. Can be negative.</param>
        /// <param name="h">The h of the rectangle. Can be negative.</param>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when any of the parameters is out of its valid range.</exception>
        public SRect(double x, double y, double w, double h)
        {
            if (double.IsNaN(x) || double.IsInfinity(x))
            {
                throw new ArgumentOutOfRangeException(nameof(x), "The origin coordinates must be finite and valid.");
            }

            if (double.IsNaN(y) || double.IsInfinity(y))
            {
                throw new ArgumentOutOfRangeException(nameof(y), "The origin coordinates must be finite and valid.");
            }

            if (w == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(w), "W cannot be zero.");
            }

            if (h == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(h), "W cannot be zero.");
            }

            _w = w;
            _h = h;
            _holes = Array.Empty<SLinearRing>();

            var geom = GenerateGeometry(w, h);
            geom.Translate(x, y);
            _shell = geom;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SRect"/> class.
        /// </summary>
        /// <param name="origin">The origin location.</param>
        /// <param name="w">The w of the rectangle.</param>
        /// <param name="h">The h of the rectangle.</param>
        public SRect(SPoint origin, double w, double h)
            : this(origin.X, origin.Y, w, h)
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
            var w = point.X - origin.X;
            var h = point.Y - origin.Y;

            if (w == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(w), "W cannot be zero.");
            }

            if (h == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(h), "H cannot be zero.");
            }

            _w = w;
            _h = h;

            var geom = GenerateGeometry(w, h);
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
        /// Gets the rectangle w.
        /// </summary>
        public double W => _w;

        /// <summary>
        /// Gets the rectangle h.
        /// </summary>
        public double H => _h;

        #endregion Properties

        #region Methods

        // Generates a rectangle geometry using its w and h.
        private static SLinearRing GenerateGeometry(double w, double h)
        {
            var p0 = new SPoint(0, 0);
            var p1 = new SPoint(0 + w, 0);
            var p2 = new SPoint(0 + w, 0 + h);
            var p3 = new SPoint(0, 0 + h);
            var p4 = p0;

            var points = new SPoint[5] { p0, p1, p2, p3, p4 };
            var geom = new SLinearRing(points);

            return geom;
        }

        #endregion Methods
    }
}
