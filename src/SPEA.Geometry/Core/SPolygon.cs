// ==================================================================================================
// <copyright file="SPolygon.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Core
{
    /// <summary>
    /// Represents a generic polygon object.
    /// </summary>
    public class SPolygon : SPath
    {
        #region Fields

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SPolygon"/> class.
        /// </summary>
        /// <param name="segments">A sequence of line segments.</param>
        public SPolygon(IEnumerable<SVector> segments)
            : base(segments)
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SPolygon"/> class.
        /// </summary>
        /// <param name="segments">A sequence of line segments.</param>
        public SPolygon(params SVector[] segments)
            : base(segments)
        {
            // Blank.
        }

        #endregion Constructors
    }
}
