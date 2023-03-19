// ==================================================================================================
// <copyright file="SPoint.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Core
{
    using SPEA.Geometry.Transform;

    /// <summary>
    /// Represents a simple point in 2D space.
    /// </summary>
    public readonly struct SPoint : IEquatable<SPoint>
    {
        #region Fields

        /// <summary>
        /// Gets the internal type of this entity.
        /// </summary>
        public const SEntityType InternalType = SEntityType.SPOINT;

        private readonly double _x;
        private readonly double _y;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SPoint"/> struct.
        /// </summary>
        public SPoint()
            : this(0, 0)
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SPoint"/> struct.
        /// </summary>
        /// <param name="x">X-coordinate of the point location.</param>
        /// <param name="y">Y-coordinate of the point location.</param>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when any of the parameters is out of its valid range.</exception>
        public SPoint(double x, double y)
        {
            if (double.IsNaN(x) || double.IsInfinity(x))
            {
                throw new ArgumentOutOfRangeException(nameof(x), "The coordinates must be finite and valid.");
            }

            if (double.IsNaN(y) || double.IsInfinity(y))
            {
                throw new ArgumentOutOfRangeException(nameof(y), "The coordinates must be finite and valid.");
            }

            _x = x;
            _y = y;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the X-coordinate of the origin.
        /// </summary>
        public double X => _x;

        /// <summary>
        /// Gets the Y-coordinate of the origin.
        /// </summary>
        public double Y => _y;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates a new <see cref="SPoint"/> as a sum of coordinates of given points.
        /// </summary>
        /// <param name="left">The first point.</param>
        /// <param name="right">The second point.</param>
        /// <returns>A new <see cref="SPoint"/>.</returns>
        public static SPoint operator +(SPoint left, SPoint right) => Add(left, right);

        /// <summary>
        /// Creates a new <see cref="SPoint"/> as a difference of coordinates of given points.
        /// </summary>
        /// <param name="left">The first point.</param>
        /// <param name="right">The second point.</param>
        /// <returns>A new <see cref="SPoint"/>.</returns>
        public static SPoint operator -(SPoint left, SPoint right) => Substract(left, right);

        /// <summary>
        /// Compares two <see cref="SPoint"/> objects.
        /// </summary>
        /// <remarks>
        /// The result specifies whether the <see cref="SPoint"/> coordinates are equal.
        /// </remarks>
        /// <param name="left">The first point.</param>
        /// <param name="right">The second point.</param>
        /// <returns><see langword="true"/> if points are equal, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(SPoint left, SPoint right) => left.Equals(right);

        /// <summary>
        /// Compares two <see cref="SPoint"/> objects.
        /// </summary>
        /// <remarks>
        /// The result specifies whether the <see cref="SPoint"/> coordinates are not equal.
        /// </remarks>
        /// <param name="left">The first point.</param>
        /// <param name="right">The second point.</param>
        /// <returns><see langword="true"/> if points are not equal, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(SPoint left, SPoint right) => !(left == right);

        /// <summary>
        /// Creates a new <see cref="SPoint"/> as a sum of coordinates of given points.
        /// </summary>
        /// <param name="left">The first point.</param>
        /// <param name="right">The second point.</param>
        /// <returns>A new <see cref="SPoint"/>.</returns>
        public static SPoint Add(SPoint left, SPoint right) => new SPoint(unchecked(left.X + right.X), unchecked(left.Y + right.Y));

        /// <summary>
        /// Creates a new <see cref="SPoint"/> as a difference of coordinates of given points.
        /// </summary>
        /// <param name="left">The first point.</param>
        /// <param name="right">The second point.</param>
        /// <returns>A new <see cref="SPoint"/>.</returns>
        public static SPoint Substract(SPoint left, SPoint right) => new SPoint(unchecked(left.X - right.X), unchecked(left.Y - right.Y));

        /// <inheritdoc/>
        public bool Equals(SPoint other)
        {
            return EqualsInternal(this, other);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is SPoint point && Equals(point);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Always throws <see cref="NotImplementedException"/>.
        /// </remarks>
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new <see cref="SPoint"/> by applying a given transformation to the current <see cref="SPoint"/>.
        /// </summary>
        /// <remarks>
        /// The point to which the transformation is applied appears on the RIGHT side: <c>[T][P]</c>.
        /// </remarks>
        /// <param name="transform">An affine transformation to apply.</param>
        /// <returns>A new transformed <see cref="SPoint"/>.</returns>
        public SPoint Transform(GeneralTransformation transform)
        {
            if (transform.IsIdentity)
            {
                return this;
            }

            var x = (transform.M00 * _x) + (transform.M01 * _y) + transform.M02;
            var y = (transform.M10 * _x) + (transform.M11 * _y) + transform.M12;

            return new SPoint(x, y);
        }

        // Performs an equality check.
        private static bool EqualsInternal(SPoint left, SPoint right)
        {
            if (left.X == right.X && left.Y == right.Y)
            {
                return true;
            }

            return false;
        }

        #endregion Methods
    }
}