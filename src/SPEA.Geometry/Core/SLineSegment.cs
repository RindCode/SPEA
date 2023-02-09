// ==================================================================================================
// <copyright file="SLineSegment.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Core
{
    /// <summary>
    /// Represents a straight line segment.
    /// </summary>
    public readonly struct SLineSegment
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SLineSegment"/> struct.
        /// </summary>
        /// <param name="start">Line start point.</param>
        /// <param name="end">Line end point.</param>
        public SLineSegment(SPoint start, SPoint end)
        {
            StartPoint = start;
            EndPoint = end;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the line start point.
        /// </summary>
        public SPoint StartPoint { get; }

        /// <summary>
        /// Gets the line end point.
        /// </summary>
        public SPoint EndPoint { get; }

        #endregion Properties
    }
}
