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
    public sealed class SRect : SPolygon
    {
        #region Fields

        /// <summary>
        /// Gets the internal type of this entity.
        /// </summary>
        public new const SEntityType InternalType = SEntityType.SRECT;

        private readonly SRect _definingObject;
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

            var geometry = GenerateGeometry(w, h);
            _shell = geometry;

            _definingObject = new SRect(this);

            geometry.Translate(x, y);
            _shell = geometry;
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
            : this(origin.X, origin.Y, point.X, point.Y)
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

        /// <summary>
        /// Initializes a new instance of the <see cref="SRect"/> class.
        /// </summary>
        /// <param name="sRect"><see cref="SRect"/> object used for a "copy".</param>
        private SRect(SRect sRect)
        {
            _w = sRect.W;
            _h = sRect.H;

            _shell = new SLinearRing(sRect.Shell.Points);

            var holes = new SLinearRing[sRect.Holes.Length];
            Array.Copy(sRect.Holes, holes, holes.Length);
            _holes = holes;

            _definingObject = this;
        }

        #endregion Constructors

        #region Properties

        /// <inheritdoc/>
        public override SLinearRing Shell => _shell;

        /// <inheritdoc/>
        public override SLinearRing[] Holes => _holes;

        /// <inheritdoc/>
        public override SRect DefiningObject => _definingObject;

        /// <summary>
        /// Gets the rectangle w.
        /// </summary>
        public double W => _w;

        /// <summary>
        /// Gets the rectangle h.
        /// </summary>
        public double H => _h;

        /// <summary>
        /// Gets the signed area of a closed ring.
        /// </summary>
        public double A0 => Shell.As;

        /// <summary>
        /// Gets the unsigned area of a closed ring.
        /// </summary>
        public double A => Shell.A;

        /// <summary>
        /// Gets the centroid location.
        /// </summary>
        public SPoint Centroid => Shell.Centroid;

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
