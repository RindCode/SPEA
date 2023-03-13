// ==================================================================================================
// <copyright file="SLinePath.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.Geometry.Core
{
    using SPEA.Geometry.Transform;

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

        private readonly SLinePath _definingObject;
        private SPoint[] _points;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SLinePath"/> class.
        /// </summary>
        public SLinePath()
        {
            _points = Array.Empty<SPoint>();
            _definingObject = new SLinePath(this);
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
            _definingObject = new SLinePath(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SLinePath"/> class.
        /// </summary>
        /// <param name="sLinePath"><see cref="SLinePath"/> object used for a "copy".</param>
        protected SLinePath(SLinePath sLinePath)
        {
            var points = new SPoint[sLinePath.Points.Length];
            Array.Copy(sLinePath.Points, points, points.Length);
            _points = sLinePath.Points;

            _definingObject = this;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the array of <see cref="SLinePath"/> coordinates.
        /// </summary>
        public SPoint[] Points => _points;

        /// <inheritdoc/>
        public override SPoint Origin => IsEmpty ? default(SPoint) : _points[0];

        /// <summary>
        /// Gets a value indicating whether the <see cref="SLinePath"/> is empty
        /// and contains no points in it.
        /// </summary>
        public override bool IsEmpty => _points.Length == 0;

        /// <inheritdoc/>
        public override SLinePath DefiningObject => _definingObject;

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

        /////// <inheritdoc/>
        ////public override SLinePath DeepCopy()
        ////{
        ////    var points = new SPoint[Points.Length];
        ////    Array.Copy(Points, points, points.Length);
        ////    return new SLinePath(points);
        ////}

        /// <inheritdoc/>
        public override void ApplyTransformation(AffineTransformation transform, TransformationType transformationType)
        {
            ArgumentNullException.ThrowIfNull(transform, nameof(transform));

            if (transform.IsIdentity)
            {
                return;
            }

            if (transformationType == TransformationType.RelativeToInitial)
            {
                var initial = new SPoint[Points.Length];
                Array.Copy(Points, initial, initial.Length);
                for (int i = 0; i < initial.Length; i++)
                {
                    initial[i] = initial[i].Transform(transform);
                }

                _points = initial;
            }
            else
            {
                for (int i = 0; i < Points.Length; i++)
                {
                    Points[i] = Points[i].Transform(transform);
                }
            }

            InvalidateCache();
        }

        #endregion Methods
    }
}
