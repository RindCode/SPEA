// ==================================================================================================
// <copyright file="SPoint.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Base
{
    /// <summary>
    /// Represents a point primitive.
    /// </summary>
    /// <remarks>
    /// <see cref="SPoint"/> is not derived from <see cref="SObject"/> since it represents the most
    /// basic entity type and any <see cref="SObject"/> is actually a set of <see cref="SPoint"/> objects.
    /// </remarks>
    public readonly struct SPoint
    {
        #region Fields

        private readonly double _x;

        private readonly double _y;

        #endregion Fields

        #region Constructors

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

        /// <summary>
        /// Initializes a new instance of the <see cref="SPoint"/> struct.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when any of the parameters is out of its valid range.</exception>
        public SPoint()
            : this(0, 0)
        {
            // Blank.
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
    }
}