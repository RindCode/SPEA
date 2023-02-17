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

        private readonly SPoint[] _points;
        private SPoint _origin;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SLinePath"/> class.
        /// </summary>
        public SLinePath()
        {
            _points = Array.Empty<SPoint>();
            _origin = default(SPoint);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SLinePath"/> class.
        /// </summary>
        /// <param name="points">A sequence of line points.</param>
        /// <exception cref="ArgumentNullException">Is thrown when <paramref name="points"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when <paramref name="points"/> has invalid number of elements.</exception>
        public SLinePath(IList<SPoint> points)
        {
            ArgumentNullException.ThrowIfNull(points);

            if (points.Count == 1)
            {
                throw new ArgumentOutOfRangeException(nameof(points), "The array must contain zero or more than one element (0 or 1+).");
            }

            _points = points.ToArray();
            _origin = points.Count > 0 ? points[0] : default(SPoint);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the array of <see cref="SLinePath"/> coordinates.
        /// </summary>
        public SPoint[] Points => _points;

        /// <inheritdoc/>
        public override SPoint Origin
        {
            get
            {
                if (IsEmpty)
                {
                    _origin = default(SPoint);
                    return _origin;
                }

                _origin = _points[0];
                return _origin;
            }

            set
            {
                if (_origin == value)
                {
                    return;
                }

                var dx = value.X - _origin.X;
                var dy = value.Y - _origin.Y;
                var transform = new TranslationTransformation(dx, dy);
                ApplyTransformation(transform);

                _origin = _points[0];  // TODO: Use this instead of value to avoid precision errors? How close are they?
            }
        }

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
        public override void ApplyTransformation(AffineTransformation transform)
        {
            ArgumentNullException.ThrowIfNull(transform);

            if (transform.IsIdentity)
            {
                return;
            }

            for (int i = 0; i < Points.Length; i++)
            {
                Points[i] = Points[i].Transform(transform);
            }
        }

        #endregion Methods
    }
}
