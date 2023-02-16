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
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SLinePath"/> class.
        /// </summary>
        /// <param name="points">A sequence of line points.</param>
        /// <exception cref="ArgumentNullException">Is thrown when <paramref name="points"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when <paramref name="points"/> has invalid number of elements.</exception>
        public SLinePath(IList<SPoint> points)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            if (points.Count == 1)
            {
                throw new ArgumentOutOfRangeException(nameof(points), "The array must contain zero or more than one element (0 or 1+).");
            }

            _points = points.ToArray();
            _origin = points[0];
        }

        /////// <summary>
        /////// Initializes a new instance of the <see cref="SLinePath"/> class.
        /////// </summary>
        /////// <param name="points">A sequence of line points.</param>
        ////public SLinePath(params SPoint[] points)
        ////{
        ////    if (points == null)
        ////    {
        ////        throw new ArgumentNullException(nameof(points));
        ////    }

        ////    if (points.Length == 1)
        ////    {
        ////        throw new ArgumentOutOfRangeException("The array must contain zero or more than one element.");
        ////    }

        ////    _points = points;
        ////}

        /////// <summary>
        /////// Initializes a new instance of the <see cref="SLinePath"/> class.
        /////// </summary>
        /////// <param name="points">A sequence of line points defined as coordinates pairs: x0, y0, x1, y1, ..., xn, yn.</param>
        ////public SLinePath(params double[] points)
        ////{
        ////    if (points == null)
        ////    {
        ////        throw new ArgumentNullException(nameof(points));
        ////    }

        ////    if (points.Length != 0 || points.Length % 2 != 0)
        ////    {
        ////        throw new ArgumentOutOfRangeException("Coordinates array cannot have zero or odd (not even) length.", nameof(points));
        ////    }

        ////    int len = points.Length / 2;
        ////    var arr = new SPoint[len];
        ////    for (int i = 0; i < len; i++)
        ////    {
        ////        var x = points[2 * i];
        ////        var y = points[(2 * i) + 1];
        ////        var p = new SPoint(x, y);
        ////        arr[i] = p;
        ////    }

        ////    _points = arr;
        ////}

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the array of <see cref="SLinePath"/> coordinates.
        /// </summary>
        public SPoint[] Points => _points;

        /// <inheritdoc/>
        public override SPoint Origin
        {
            get => _origin;
            set
            {
                if (_origin == value)
                {
                    return;
                }

                var dx = value.X - _origin.X;
                var dy = value.Y - _origin.Y;
                var transform = new TranslateTransform(dx, dy);
                ApplyTransformation(transform);

                _origin = Points[0];  // TODO: Use this instead of value to avoid precision errors? How close are they?
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

                return Points[0] == Points[^1];
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
        public override void ApplyTransformation(AffineTransform transform)
        {
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
