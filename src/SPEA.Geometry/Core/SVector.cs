﻿// ==================================================================================================
// <copyright file="SVector.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Core
{
    /// <summary>
    /// Represents a vector object that has a direction and a magnitude.
    /// </summary>
    public readonly struct SVector
    {
        #region Fields

        /// <summary>
        /// Gets the internal type of this entity.
        /// </summary>
        public const SEntityType InternalType = SEntityType.SVECTOR;

        private readonly SPoint _p;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SVector"/> struct.
        /// </summary>
        public SVector()
            : this(default(SPoint))
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SVector"/> struct.
        /// </summary>
        /// <param name="x">X-coordinate of the terminal point.</param>
        /// <param name="y">Y-coordinate of the terminal point.</param>
        public SVector(double x, double y)
        {
            _p = new SPoint(x, y);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SVector"/> struct.
        /// </summary>
        /// <param name="point">Vector terminal point.</param>
        public SVector(SPoint point)
            : this(point.X, point.Y)
        {
            // Blank.
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a new zero vector.
        /// </summary>
        public static SVector Zero => new SVector(0.0, 0.0);

        /// <summary>
        /// Gets a new (1.0, 1.0) vector.
        /// </summary>
        public static SVector One => new SVector(1.0, 1.0);

        /// <summary>
        /// Gets a new (1.0, 0.0) vector.
        /// </summary>
        public static SVector UnitX => new SVector(1.0, 0.0);

        /// <summary>
        /// Gets a new (0.0, 1.0) vector.
        /// </summary>
        public static SVector UnitY => new SVector(0.0, 1.0);

        /// <summary>
        /// Gets the X component of the terminal point.
        /// </summary>
        public double X => _p.X;

        /// <summary>
        /// Gets the Y component of the terminal point.
        /// </summary>
        public double Y => _p.Y;

        /// <summary>
        /// Gets the vector squared length.
        /// </summary>
        public double LengthSquared => (_p.X * _p.X) + (_p.Y * _p.Y);

        /// <summary>
        /// Gets the vector length.
        /// </summary>
        public double Length => Math.Sqrt(LengthSquared);

        #endregion Properties

        #region Methods

        /// <summary>
        /// Normalizes a given vector.
        /// </summary>
        /// <param name="v">A vector to normalize.</param>
        /// <returns>A normalized <see cref="SVector"/>.</returns>
        public static SVector Normalize(SVector v)
        {
            var rlen = 1.0 / v.Length;
            var x = v.X * rlen;
            var y = v.Y * rlen;

            return new SVector(x, y);
        }

        /// <summary>
        /// Calculates the dot product of two vectors.
        /// </summary>
        /// <param name="v1">A first vector.</param>
        /// <param name="v2">A second vector.</param>
        /// <returns>The dot product.</returns>
        public static double Dot(SVector v1, SVector v2)
        {
            return (v1.X * v2.X) + (v1.Y * v2.Y);
        }

        #endregion Methods
    }
}
