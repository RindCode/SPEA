// ==================================================================================================
// <copyright file="SLinearRing.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Core
{
    /// <summary>
    /// Represents a closed linear path.
    /// </summary>
    public class SLinearRing : SLinePath
    {
        #region Fields

        /// <summary>
        /// Gets the internal type of this entity.
        /// </summary>
        public new const SEntityType InternalType = SEntityType.SLINEPATH;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SLinearRing"/> class.
        /// </summary>
        public SLinearRing()
            : base()
        {
            // Blank.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SLinearRing"/> class.
        /// </summary>
        /// <param name="points">A sequence of line points.</param>
        /// <exception cref="ArgumentException">Is thrown when <paramref name="points"/> array doesn't form a closed path.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when <paramref name="points"/> has invalid number of elements.</exception>
        public SLinearRing(IList<SPoint> points)
            : base(points)
        {
            if (!base.IsClosed)
            {
                throw new ArgumentException("The array must form a closed path (the last element must be equal to the first one).");
            }

            if (points.Count > 0 && points.Count < 3)
            {
                throw new ArgumentOutOfRangeException(nameof(points), "The array must contain zero or more than two elements (0 or 3+).");
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the current <see cref="SLinePath"/> is closed.
        /// </summary>
        public override bool IsClosed
        {
            get
            {
                if (IsEmpty)
                {
                    return true;  // Empty ring is defined as closed.
                }

                return base.IsClosed;
            }
        }

        #endregion Properties
    }
}
