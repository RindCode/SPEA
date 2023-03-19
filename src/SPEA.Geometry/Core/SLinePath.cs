// ==================================================================================================
// <copyright file="SLinePath.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Core
{
    using SPEA.Geometry.Misc;
    using SPEA.Geometry.Systems;

    /// <summary>
    /// Represents a sequence of two or more points.
    /// </summary>
    public class SLinePath : SObject
    {
        #region Fields

        /// <summary>
        /// Gets the internal type of this entity.
        /// </summary>
        public new const SEntityType InternalType = SEntityType.SLINEPATH;

        private readonly CartesianSystem _system = new CartesianSystem();
        private SPoint[] _points;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SLinePath"/> class.
        /// </summary>
        public SLinePath()
        {
            _points = Array.Empty<SPoint>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SLinePath"/> class.
        /// </summary>
        /// <param name="points">A sequence of line points.</param>
        /// <exception cref="ArgumentNullException">Is thrown when <paramref name="points"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when <paramref name="points"/> has invalid number of elements.</exception>
        public SLinePath(IList<SPoint> points)
        {
            ArgumentNullException.ThrowIfNull(points, nameof(points));

            if (points.Count == 1)
            {
                throw new ArgumentOutOfRangeException(nameof(points), "The array must contain zero or more than one element (0 or 1+).");
            }

            _points = points.ToArray();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the array of <see cref="SLinePath"/> coordinates.
        /// </summary>
        public SPoint[] Points => _points;

        /// <inheritdoc/>
        public override CartesianSystem LocalSystem => _system;

        /// <inheritdoc/>
        public override SPoint Origin => LocalSystem.Origin;

        /// <summary>
        /// Gets a value indicating whether the <see cref="SLinePath"/> is empty
        /// and contains no points in it.
        /// </summary>
        public override bool IsEmpty => _points.Length == 0;

        /// <summary>
        /// Gets a value indicating whether the currect <see cref="SLinePath"/> is closed.
        /// </summary>
        public virtual bool IsClosed
        {
            get
            {
                if (IsEmpty)
                {
                    return false;
                }

                return _points[0] == _points[^1];
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="SLinePath"/> is simple.
        /// </summary>
        public virtual bool IsSimple => true;  // TODO: implementation required (no anomalous geometric points, such as self intersection or self tangency).

        /// <summary>
        /// Gets a value indicating whether the <see cref="SLinePath"/> forms a ring.
        /// </summary>
        public bool IsRing => IsClosed && IsSimple;

        #endregion Properties

        #region Methods

        /// <inheritdoc/>
        public override BoundingBox GetBoundingBox()
        {
            var minX = double.MaxValue;
            var minY = double.MaxValue;
            var maxX = double.MinValue;
            var maxY = double.MinValue;

            for (int i = 0; i < Points.Length; i++)
            {
                minX = Math.Min(minX, Points[i].X);
                minY = Math.Min(minY, Points[i].Y);
                maxX = Math.Max(maxX, Points[i].X);
                maxY = Math.Max(maxY, Points[i].Y);
            }

            return new BoundingBox(minX, maxY, maxX, minY);
        }

        #endregion Methods
    }
}
