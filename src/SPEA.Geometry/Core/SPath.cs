// ==================================================================================================
// <copyright file="SPath.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Core
{
    /// <summary>
    /// Represents a collection straight line objects, forming a single logical path.
    /// </summary>
    public class SPath : SObject
    {
        #region Fields

        private readonly SVector[] _segments;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SPath"/> class.
        /// </summary>
        /// <param name="segments">A sequence of line segments.</param>
        public SPath(IEnumerable<SVector> segments)
            : this(segments.ToArray())
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SPath"/> class.
        /// </summary>
        /// <param name="segments">A sequence of line segments.</param>
        public SPath(params SVector[] segments)
        {
            _segments = segments ?? throw new ArgumentNullException(nameof(segments));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the array containing <see cref="SPath"/> coordinates.
        /// </summary>
        public SVector[] Segments => _segments;

        #endregion properties
    }
}
