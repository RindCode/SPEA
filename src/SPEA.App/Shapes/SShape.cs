// ==================================================================================================
// <copyright file="SShape.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Shapes
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using SPEA.App.Utils.Helpers;

    /// <summary>
    /// Represents a lightweight implementation of <see cref="System.Windows.Shapes"/> class,
    /// which is more flexible for extension when overridden.
    /// </summary>
    public abstract class SShape : FrameworkElement
    {
        #region Fields

        private Pen? _pen;
        private Geometry? _renderedGeometry = Geometry.Empty;

        #endregion Fields

        #region Dependency Properties

        /// <summary>
        /// DependencyProperty for <see cref="Fill"/> property.
        /// </summary>
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register(
                "Fill",
                typeof(Brush),
                typeof(SShape),
                new FrameworkPropertyMetadata(
                    default(Brush),
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        /// <summary>
        /// Gets or sets a filling brush.
        /// </summary>
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        /// <summary>
        /// DependencyProperty for <see cref="Stroke"/> property.
        /// </summary>
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register(
                "Stroke",
                typeof(Brush),
                typeof(SShape),
                new FrameworkPropertyMetadata(
                    default(Brush),
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
                    new PropertyChangedCallback(OnPenChanged)));

        /// <summary>
        /// Gets or sets a stroke brush.
        /// </summary>
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        /// <summary>
        /// DependencyProperty for <see cref="StrokeThickness"/> property.
        /// </summary>
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register(
                "StrokeThickness",
                typeof(double),
                typeof(SShape),
                new FrameworkPropertyMetadata(
                    1.0d,
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(OnPenChanged),
                    new CoerceValueCallback(CoerceStrokeThickness)));

        /// <summary>
        /// Gets or sets a stroke thickness value.
        /// </summary>
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        #endregion Dependency Properties

        #region Properties

        /// <summary>
        /// Gets the geometry that defines this shape.
        /// </summary>
        /// <remarks>
        /// See <see cref="CacheDefiningGeometry"/> description for more information.
        /// </remarks>
        protected abstract Geometry DefiningGeometry { get; }

        /// <summary>
        /// Gets the final rendered geometry - a frozen copy of the cached value.
        /// </summary>
        protected virtual Geometry RenderedGeometry
        {
            get
            {
                EnsureRenderedGeometry();

                var geometry = _renderedGeometry?.CloneCurrentValue();
                if (geometry == null || geometry == Geometry.Empty)
                {
                    return Geometry.Empty;
                }

                if (ReferenceEquals(geometry, _renderedGeometry))
                {
                    geometry = geometry.Clone();
                    geometry.Freeze();
                }

                return geometry;
            }
        }

        // Gets the cached shape pen.
        internal Pen? Pen
        {
            get => GetPen();
            private set => _pen = value;
        }

        // Indicates whether the pen will be rendered.
        private bool DoNotRenderPen
        {
            get
            {
                var strokeThickness = StrokeThickness;
                return (Stroke == null) || DoubleUtilHelper.IsZero(strokeThickness) || double.IsNaN(strokeThickness);
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Determines whether a given size is empty or invalid.
        /// </summary>
        /// <param name="size"><see cref="Size"/> to be tested.</param>
        /// <returns><see langword="true"/> if invalid or empty, otherwise <see langword="false"/>.</returns>
        public static bool IsSizeInvalidOrEmpty(Size size)
        {
            return size.IsEmpty || double.IsNaN(size.Width) || double.IsNaN(size.Height);
        }

        /// <summary>
        /// Resets the rendered geometry.
        /// </summary>
        protected void ResetRenderedGeometry()
        {
            _renderedGeometry = null;
        }

        /// <summary>
        /// Ensures <see cref="DefiningGeometry"/> contains geometry and caches its value.
        /// </summary>
        /// <exception cref="NullReferenceException">Is thrown if <see cref="DefiningGeometry"/> is <see langword="null"/>.</exception>
        protected void EnsureRenderedGeometry()
        {
            if (_renderedGeometry == null)
            {
                _renderedGeometry = DefiningGeometry ?? throw new NullReferenceException($"{nameof(DefiningGeometry)} must not be null.");
            }
        }

        /// <summary>
        /// Provides an opportunity for derived classes to implement their own logic
        /// to cache <see cref="DefiningGeometry"/> value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// By default, this method has no logic in it.
        /// </para>
        /// <para>
        /// This method is called at the beginning of <see cref="MeasureOverride(Size)"/>
        /// (if it wasn't overridden with another logic) and is intended to work in conjunction with
        /// <see cref="DefiningGeometry"/> property, since this property is used to calculate the shape
        /// layout and then render it. Use this method to define the shape geometry, cache it in some
        /// field and return this field in <see cref="DefiningGeometry"/> getter.
        /// </para>
        /// </remarks>
        protected virtual void CacheDefiningGeometry()
        {
            // Blank.
        }

        /// <summary>
        /// Gets the natural size of the geometry that defines this shape.
        /// </summary>
        /// <returns><see cref="Size"/> of the shape.</returns>
        /// <exception cref="NullReferenceException">Is thrown if <see cref="DefiningGeometry"/> is <see langword="null"/>.</exception>
        protected virtual Size GetNaturalSize()
        {
            var geometry = DefiningGeometry ?? throw new NullReferenceException($"{nameof(DefiningGeometry)} must not be null.");

            // Mirrors the default MS Shape class behavior, where dashing is not considered for computing
            // the layout size. It is mentioned there that the computation will be faster and more stable even
            // though produces slightly different bounds.

            DashStyle? style = null;
            var pen = GetPen();
            if (pen != null)
            {
                style = pen.DashStyle;

                if (style != null)
                {
                    pen.DashStyle = null;
                }
            }

            var bounds = geometry.GetRenderBounds(pen);

            if (style != null && pen != null)
            {
                pen.DashStyle = style;
            }

            var naturalSize = new Size(Math.Max(bounds.Right, 0), Math.Max(bounds.Bottom, 0));

            return naturalSize;
        }

        /// <summary>
        /// Updates <see cref="UIElement.DesiredSize"/> of the shape. Called by parent <see cref="UIElement"/> during the first pass of layout.
        /// </summary>
        /// <param name="availableSize">
        /// The available size that this element can give to child elements.
        /// Infinity can be specified as a value to indicate that the element will size to whatever content is available.
        /// </param>
        /// <returns>The size that this element determines it needs during layout, based on its calculations of child element sizes.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            CacheDefiningGeometry();

            var desiredSize = GetNaturalSize();
            if (IsSizeInvalidOrEmpty(desiredSize))
            {
                // Do not draw anything.
                desiredSize = new Size(0, 0);
                _renderedGeometry = Geometry.Empty;
            }

            return desiredSize;
        }

        /// <summary>
        /// When overridden in a derived class, positions child elements and determines a size
        /// for a <see cref="FrameworkElement"/> derived class.
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this elementshould use to arrange itself and its children.</param>
        /// <returns>The actual size used.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            ResetRenderedGeometry();

            if (IsSizeInvalidOrEmpty(finalSize))
            {
                // Do not draw anything.
                finalSize = new Size(0, 0);
                _renderedGeometry = Geometry.Empty;
            }

            return finalSize;
        }

        /// <inheritdoc/>
        protected override void OnRender(DrawingContext drawingContext)
        {
            EnsureRenderedGeometry();

            if (_renderedGeometry != Geometry.Empty)
            {
                // Do not access the chached pen value here. Use the property instead.
                drawingContext.DrawGeometry(Fill, Pen, _renderedGeometry);
            }
        }

        // Is called whenever Stroke-related property is invalidated.
        // That means the chached pen should be re-calculated.
        private static void OnPenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SShape)d).Pen = null;
        }

        // Coerces the stroke thickness value.
        private static object CoerceStrokeThickness(DependencyObject d, object value)
        {
            var thickness = (double)value;
            if (thickness < 0)
            {
                return 0.0d;
            }

            return thickness;
        }

        // Generates a pen object to be used in rendering.
        private Pen? GetPen()
        {
            if (DoNotRenderPen)
            {
                return null;
            }

            if (_pen == null)
            {
                _pen = new Pen();
                _pen.Brush = Stroke;
                _pen.Thickness = StrokeThickness;
            }

            return _pen;
        }

        #endregion Methods
    }
}
