// ==================================================================================================
// <copyright file="SRectPrimitive.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Shapes
{
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Represents a rectangle primitive shape.
    /// </summary>
    public sealed class SRectPrimitive : SShape
    {
        #region Fields

        private Geometry _geometry = Geometry.Empty;

        #endregion Fields

        #region Constructors

        ////static SRectPrimitive()
        ////{
        ////    // Override FrameworkElement Width and Height DPs metadata
        ////    // to add FrameworkPropertyMetadataOptions.AffectsRender flag to them.

        ////    WidthProperty.OverrideMetadata(
        ////        typeof(SRectPrimitive),
        ////        new FrameworkPropertyMetadata(
        ////            double.NaN,
        ////            FrameworkPropertyMetadataOptions.AffectsMeasure |
        ////            FrameworkPropertyMetadataOptions.AffectsRender));

        ////    HeightProperty.OverrideMetadata(
        ////        typeof(SRectPrimitive),
        ////        new FrameworkPropertyMetadata(
        ////            double.NaN,
        ////            FrameworkPropertyMetadataOptions.AffectsMeasure |
        ////            FrameworkPropertyMetadataOptions.AffectsRender));
        ////}

        /// <summary>
        /// Initializes a new instance of the <see cref="SRectPrimitive"/> class.
        /// </summary>
        public SRectPrimitive()
        {
            // Blank.
        }

        #endregion Constructors

        #region Dependency Properties

        /// <summary>
        /// DependencyProperty for <see cref="FillRule"/> property.
        /// </summary>
        public static readonly DependencyProperty FillRuleProperty =
            DependencyProperty.Register(
                "FillRule",
                typeof(FillRule),
                typeof(SRectPrimitive),
                new FrameworkPropertyMetadata(
                    FillRule.EvenOdd,
                    FrameworkPropertyMetadataOptions.AffectsMeasure),
                new ValidateValueCallback(IsFillRuleValid));

        /// <summary>
        /// Gets or sets a rectangle fill rule.
        /// </summary>
        public FillRule FillRule
        {
            get { return (FillRule)GetValue(FillRuleProperty); }
            set { SetValue(FillRuleProperty, value); }
        }

        /// <summary>
        /// DependencyProperty for <see cref="W"/> property.
        /// </summary>
        public static readonly DependencyProperty WProperty =
            DependencyProperty.Register(
                "W",
                typeof(double),
                typeof(SRectPrimitive),
                new FrameworkPropertyMetadata(
                    1.0d,
                    FrameworkPropertyMetadataOptions.AffectsMeasure),
                new ValidateValueCallback(IsWidthHeightValid));

        /// <summary>
        /// Gets or sets a rectangle width.
        /// </summary>
        public double W
        {
            get { return (double)GetValue(WProperty); }
            set { SetValue(WProperty, value); }
        }

        /// <summary>
        /// DependencyProperty for <see cref="H"/> property.
        /// </summary>
        public static readonly DependencyProperty HProperty =
            DependencyProperty.Register(
                "H",
                typeof(double),
                typeof(SRectPrimitive),
                new FrameworkPropertyMetadata(
                    1.0d,
                    FrameworkPropertyMetadataOptions.AffectsMeasure),
                new ValidateValueCallback(IsWidthHeightValid));

        /// <summary>
        /// Gets or sets a rectangle width.
        /// </summary>
        public double H
        {
            get { return (double)GetValue(HProperty); }
            set { SetValue(HProperty, value); }
        }

        #endregion Dependency Properties

        #region Properties

        /// <inheritdoc/>
        protected override Geometry DefiningGeometry => _geometry;

        #endregion Properties

        #region Methods

        /// <inheritdoc/>
        protected override void CacheDefiningGeometry()
        {
            var start = new Point(0, 0);
            Point[] points = new Point[3]
            {
                new Point(W, start.X),
                new Point(W, H),
                new Point(start.X, H),
            };

            var pathFigure = new PathFigure();
            pathFigure.StartPoint = start;
            pathFigure.Segments.Add(new PolyLineSegment(points, true));
            pathFigure.IsClosed = true;

            var geometry = new PathGeometry();
            geometry.Figures.Add(pathFigure);
            geometry.FillRule = FillRule;

            _geometry = geometry;
        }

        // Validates FillRule enum.
        private static bool IsFillRuleValid(object value)
        {
            FillRule v = (FillRule)value;

            return v == FillRule.EvenOdd || v == FillRule.Nonzero;
        }

        // Validates width and height values.
        private static bool IsWidthHeightValid(object value)
        {
            double v = (double)value;
            return !double.IsNaN(v) && v > 0.0d && !double.IsPositiveInfinity(v);
        }

        #endregion Methods
    }
}
