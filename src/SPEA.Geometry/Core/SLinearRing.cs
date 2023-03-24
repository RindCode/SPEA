// ==================================================================================================
// <copyright file="SLinearRing.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

using SPEA.Geometry.Transform;

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

        ////// Cached values.
        ////private double? _signedArea = null;
        ////private double? _unsignedArea = null;
        ////private SPoint? _centroid = null;

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

        // Copy constructor.
        private SLinearRing(SLinearRing template)
            : this(template.Points)
        {
            LocalSystem.TransformInGlobal(template.LocalSystem.GlobalTransform, TransformAction.Replace);
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

        /////// <summary>
        /////// Gets the signed area of a closed ring.
        /////// </summary>
        ////public double As
        ////{
        ////    get
        ////    {
        ////        if (!_signedArea.HasValue)
        ////        {
        ////            _signedArea = CalculateSignedArea();
        ////        }

        ////        return _signedArea.Value;
        ////    }
        ////}

        /////// <summary>
        /////// Gets the unsigned area of a closed ring.
        /////// </summary>
        ////public double A
        ////{
        ////    get
        ////    {
        ////        if (!_unsignedArea.HasValue)
        ////        {
        ////            if (!_signedArea.HasValue)
        ////            {
        ////                _signedArea = CalculateSignedArea();
        ////            }

        ////            _unsignedArea = Math.Abs(_signedArea.Value);
        ////        }

        ////        return _unsignedArea.Value;
        ////    }
        ////}

        /////// <summary>
        /////// Gets the centroid location.
        /////// </summary>
        ////public SPoint Centroid
        ////{
        ////    get
        ////    {
        ////        if (!_centroid.HasValue)
        ////        {
        ////            _centroid = CalculateCentroid();
        ////        }

        ////        return _centroid.Value;
        ////    }
        ////}

        #endregion Properties

        #region Methods

        /// <inheritdoc/>
        public override SLinearRing DeepCopy()
        {
            return new SLinearRing(this);
        }

        /////// <inheritdoc/>
        ////protected override void InvalidateCache()
        ////{
        ////    _signedArea = null;
        ////    _unsignedArea = null;
        ////    _centroid = null;

        ////    base.InvalidateCache();
        ////}

        ////// Calculates the signed area.
        ////private double CalculateSignedArea()
        ////{
        ////    if (Points.Length == 0)
        ////    {
        ////        return 0.0;
        ////    }

        ////    double area = 0.0;
        ////    for (int i = 0; i < Points.Length - 1; i++)
        ////    {
        ////        area += (Points[i].X * Points[i + 1].Y) - (Points[i + 1].X * Points[i].Y);
        ////    }

        ////    return 0.5 * area;
        ////}

        ////// Calculates the centroid location.
        ////private SPoint CalculateCentroid()
        ////{
        ////    if (Points.Length == 0)
        ////    {
        ////        return default;
        ////    }

        ////    double s1 = 0.0;
        ////    double s2 = 0.0;
        ////    for (int i = 0; i < Points.Length - 1; i++)
        ////    {
        ////        var t = (Points[i].X * Points[i + 1].Y) - (Points[i + 1].X * Points[i].Y);
        ////        s1 += (Points[i].X + Points[i + 1].X) * t;
        ////        s2 += (Points[i].Y + Points[i + 1].Y) * t;
        ////    }

        ////    var area = As;
        ////    var factor = 1 / (6 * area);

        ////    var cx = factor * s1;
        ////    var cy = factor * s2;

        ////    return new SPoint(cx, cy);
        ////}

        #endregion Methods
    }
}
