// ==================================================================================================
// <copyright file="SRect.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Primitives
{
    using SPEA.Geometry.Core;
    using SPEA.Geometry.Misc;
    using SPEA.Geometry.Transform;

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

            _shell = GenerateGeometry(w, h);
            LocalSystem.TransformInGlobal(new TranslationTransformation(x, y));
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

        // Copy constructor.
        private SRect(SRect template)
            : this(template.Origin.X, template.Origin.Y, template.W, template.H)
        {
            LocalSystem.TransformInGlobal(template.LocalSystem.GlobalTransform, TransformAction.Replace);
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

        /// <summary>
        /// Creates a new <see cref="SRect"/> object similar to a provided one,
        /// but using different width and height.
        /// </summary>
        /// <remarks>
        /// A new object's transformation and origin location will be preserved.
        /// </remarks>
        /// <param name="example">An object used as an example.</param>
        /// <param name="w">A new width.</param>
        /// <param name="h">A new height.</param>
        /// <returns>A new <see cref="SRect"/> object.</returns>
        public static SRect SameAs(SRect example, double w, double h)
        {
            var result = new SRect(example.Origin.X, example.Origin.Y, w, h);
            result.LocalSystem.TransformInGlobal(example.LocalSystem.GlobalTransform, TransformAction.Replace);

            return result;
        }

        /// <inheritdoc/>
        public override SRect DeepCopy()
        {
            return new SRect(this);
        }

        /// <inheritdoc/>
        public override BoundingBox GetBoundingBox()
        {
            return base.GetBoundingBox();
        }

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
