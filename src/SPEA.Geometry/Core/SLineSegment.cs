// ==================================================================================================
// <copyright file="SLineSegment.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Core
{
    /// <summary>
    /// Represents a straight line segment defined by two <see cref="SPoint"/> points.
    /// </summary>
    public readonly struct SLineSegment
    {
        #region Fields

        /// <summary>
        /// Gets the internal type of this entity.
        /// </summary>
        public const SEntityType InternalType = SEntityType.SLINESEG;

        private readonly SPoint _p0;
        private readonly SPoint _p1;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SLineSegment"/> struct.
        /// </summary>
        /// <param name="p0">The line start point.</param>
        /// <param name="p1">The line end point.</param>
        public SLineSegment(SPoint p0, SPoint p1)
        {
            _p0 = p0;
            _p1 = p1;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the line p0 point.
        /// </summary>
        public SPoint P0 => _p0;

        /// <summary>
        /// Gets the line p1 point.
        /// </summary>
        public SPoint P1 => _p1;

        #endregion Properties
    }
}
