// ==================================================================================================
// <copyright file="InfinitePanel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Controls.SViewport
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using SPEA.App.Utils.Extensions;

    /// <summary>
    /// Provides scrolling functionality by implementing <see cref="IScrollInfo"/> interface,
    /// as well as infinite panning and zooming.
    /// </summary>
    /// <remarks>
    /// This panel acts an additional layer between the main control <see cref="SViewportControl"/> and
    /// <see cref="SViewportItemsHostControl"/> (which in turn, acts as a host for <see cref="SElementItemContainer"/> item containers).
    /// It must be wrapped by <see cref="ScrollViewer"/> inside <see cref="SViewportControl"/>
    /// <see cref="ControlTemplate"/> with <see cref="ScrollViewer.CanContentScroll"/> property set to <see langword="true"/>.
    /// </remarks>
    public partial class InfinitePanel : ContentControl
    {
        // For additional information see the following useful links:
        // 1. https://www.codeproject.com/Articles/85603/A-WPF-custom-control-for-zooming-and-panning
        // 2. BenCon's WebLog, "IScrollInfo in Avalon" in (4) parts.
        //    https://web.archive.org/web/20150128050816/http://blogs.msdn.com/b/bencon/archive/2006/01/05/509991.aspx
        // 3. https://github.com/mircea21S/RichCanvas

        #region Fields

        // Defined as a mandatory part name of a parent ControlTemplate.
        private const string ItemsHostPartName = "PART_ItemsHost";
        private const string HorizontalScrollBarPartName = "PART_HorizontalScrollBar";
        private const string VerticalScrollBarPartName = "PART_VerticalScrollBar";

        private const double _defaultContentScale = 1.0;
        private const double _defaultScrollFactor = 0.05;
        private const double _defaultZoomFactor = 1.1;

        private TranslateTransform? _translateTransform;
        private ScaleTransform? _scaleTransform;

        private SViewportControl? _itemsOwner;
        private FrameworkElement? _content;
        private Size _extent = new Size(0, 0);  // unscaled
        private Size _offset = new Size(0, 0);  // unscaled
        private Size _viewport = new Size(0, 0);

        #endregion Fields

        #region Properties

        /////// <summary>
        /////// Gets the parent <see cref="ItemsControl"/>.
        /////// </summary>
        ////public SViewportControl? ItemsOwner => _itemsOwner;

        public double ContentScale => _itemsOwner != null ? _itemsOwner.ContentScale : _defaultContentScale;

        public double ScrollFactor => _itemsOwner != null ? _itemsOwner.ScrollFactor : _defaultScrollFactor;

        //internal Size Extent => UnscaledExtent;

        //internal Size Offset => _offset;

        //internal Size Viewport => _viewport;

        private double? LeftMostBoundary => _itemsOwner?.ItemsHost?.LeftMostElement?.BoundingBox.Left;

        private double? TopMostBoundary => _itemsOwner?.ItemsHost?.TopMostElement?.BoundingBox.Top;

        private double? RightMostBoundary => _itemsOwner?.ItemsHost?.RightMostElement?.BoundingBox.Right;

        private double? BottomMostBoundary => _itemsOwner?.ItemsHost?.BottomMostElement?.BoundingBox.Bottom;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Internal initializer. Must be called from a parent <see cref="SViewportControl"/>.
        /// </summary>
        /// <param name="obj">A reference to a parent control.</param>
        internal void Initialize(SViewportControl obj)
        {
            _itemsOwner = obj;
        }

        /// <summary>
        /// Scrolls and translates the <see cref="ScrollOwner"/> viewport in a horizontal direction.
        /// </summary>
        /// <param name="value">A value the content will be scrolled by.</param>
        public void TranslateHorizontally(double value)
        {
            if (_itemsOwner == null)
            {
                return;
            }

            var calculatedOffset = _offset.Width + value;

            // The viewport border is moved outside the extent border (negative or positive direction).
            // Therefore we adjust the extent, but clamp the offset in a valid range (coerce part).
            var isOutsideExtent = calculatedOffset + _viewport.Width > _extent.Width;
            if (calculatedOffset < 0 || isOutsideExtent)
            {
                _extent.Width += Math.Abs(value);
            }

            // The viewport border is moved towards the content borders (negative or positive direction),
            // but the extent border is still remain outside of it.
            var contentBounds = _content.GetBoundingBox(_itemsOwner);  // extension method
            var isOutsideContent = _extent.Width > contentBounds.X + contentBounds.Width;
            if (contentBounds.X > 0 || isOutsideContent)
            {
                _extent.Width -= Math.Abs(value);
            }

            // If we remain inside the extent borders, it will be adjusted (reduced) until
            // it reaches the content bounding box, but never smaller than that.
            ////var contentBounds = GetBoundsRelativeTo(_content, _itemsOwner);
            ////var isInsideContentBounds = contentBounds.X - calculatedOffset < 0;
            ////if (contentBounds.X > 0 && isInsideContentBounds)
            ////{
            ////    UnscaledExtent.W -= Math.Abs(contentBounds.X - calculatedOffset);
            ////}

            var coercedOffset = CoerceHorizontalOffset(calculatedOffset);
            SetHorizontalOffset(coercedOffset);

            UpdateHorizontalTranslation(value);
            UpdateBackgroundViewportHorizontalTranslation(value);

            ScrollOwner?.InvalidateScrollInfo();
        }

        /// <summary>
        /// Scrolls and translates the <see cref="ScrollOwner"/> viewport in a vertical direction.
        /// </summary>
        /// <param name="valueY">A value the content will be scrolled by.</param>
        public void TranslateVertically(double valueY)
        {
            if (_itemsOwner == null)
            {
                return;
            }

            var offsetNewY = _offset.Height + valueY;
            offsetNewY = CoerceVerticalOffset(offsetNewY);
            SetVerticalOffset(offsetNewY);

            UpdateVerticalTranslation(valueY);

            ScrollOwner?.InvalidateScrollInfo();
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            Debug.WriteLine($"PANEL APPLY TEMPLATE");

            base.OnApplyTemplate();

            _content = _itemsOwner?.Template.FindName(ItemsHostPartName, _itemsOwner) as FrameworkElement;
            if (_content != null)
            {
                _translateTransform = new TranslateTransform();
                _scaleTransform = new ScaleTransform();
                ////_scaleTransform = _itemsOwner == null ? new ScaleTransform() : new ScaleTransform(_itemsOwner.ContentScale, _itemsOwner.ContentScale);

                var trasnformGroup = new TransformGroup();
                trasnformGroup.Children.Add(_translateTransform);
                trasnformGroup.Children.Add(_scaleTransform);
                _content.RenderTransform = trasnformGroup;
            }
        }

        /////// <inheritdoc/>
        ////protected override Size MeasureOverride(Size availableSize)
        ////{
        ////    // This control naturally provides the infinite available space to its children.
        ////    var infiniteSize = new Size(double.PositiveInfinity, double.PositiveInfinity);

        ////    // The default behavior of the Content/ContentControl MeasureOverride is
        ////    // to measure only the first visual child (in this case - items host control).
        ////    var contentSize = base.MeasureOverride(infiniteSize);

        ////    // Make sure we give enough space requested by the child element.
        ////    if (contentSize != _extent)
        ////    {
        ////        _extent = contentSize;
        ////        ScrollOwner?.InvalidateScrollInfo();
        ////    }

        ////    // Update viewport size based on availableSize provided by ScrollViewer.
        ////    //UpdateViewportSize(availableSize);

        ////    // Returning PositiveInfinity will raise InvalidOperationException, thus not allowed.
        ////    // Just use child dimensions to calculate the desired size in such case.

        ////    var width = availableSize.Width;
        ////    if (width == double.PositiveInfinity)
        ////    {
        ////        width = contentSize.Width;
        ////    }

        ////    var height = availableSize.Height;
        ////    if (height == double.PositiveInfinity)
        ////    {
        ////        height = contentSize.Height;
        ////    }

        ////    var desiredSize = new Size(width, height);

        ////    return desiredSize;
        ////}

        /// <inheritdoc/>
        protected override Size MeasureOverride(Size availableSize)
        {
            Debug.WriteLine($"PANEL MEASURE OVERRIDE = {availableSize}");

            return base.MeasureOverride(availableSize);
        }

        /// <inheritdoc/>
        protected override Size ArrangeOverride(Size finalSize)
        {
            Debug.WriteLine($"PANEL ARRANGE OVERRIDE = {finalSize}");

            if (ScrollOwner != null)
            {
                if (_viewport != finalSize)
                {
                    _viewport = finalSize;
                }

                ScrollOwner.InvalidateScrollInfo();
            }

            Debug.WriteLine($"extent:   w={_extent.Width,8:F3}, h={_extent.Height,8:F3}");
            Debug.WriteLine($"offset:   w={_offset.Width,8:F3}, h={_offset.Height,8:F3}");
            Debug.WriteLine($"viewport: w={_viewport.Width,8:F3}, h={_viewport.Height,8:F3}");

            return base.ArrangeOverride(finalSize);
        }

        // 
        private void HorizontalScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            var oldOffset = _offset.Width;
            var newOffset = e.NewValue;
            var delta = newOffset - oldOffset;

            TranslateHorizontally(delta);
        }

        // Translates the horizontal translation by using the provided offes value.
        private void UpdateHorizontalTranslation(double value)
        {
            if (_translateTransform != null)
            {
                _translateTransform.X -= value;
            }
        }

        // Updates the vertical translation by using the provided offes value.
        private void UpdateVerticalTranslation(double value)
        {
            if (_translateTransform != null)
            {
                _translateTransform.Y -= value;
            }
        }

        // Horizontally translates ScrollViewer background (grids) by using the provided offes value.
        private void UpdateBackgroundViewportHorizontalTranslation(double value)
        {
            var minorGridViewport = _itemsOwner.MinorGridViewport;
            minorGridViewport.X -= value;
            _itemsOwner.MinorGridViewport = minorGridViewport;

            var majorGridViewport = _itemsOwner.MajorGridViewport;
            majorGridViewport.X -= value;
            _itemsOwner.MajorGridViewport = majorGridViewport;
        }

        // Coerces the horizontal offset value.
        private double CoerceHorizontalOffset(double offsetOld)
        {
            // Size() doesn't allow negative values.
            // Offset value is constrained between 0 and (extent.W - viewport.W).
            var offsetMin = 0.0d;
            var offsetMax = Math.Max(offsetMin, _extent.Width - _viewport.Width);
            var offsetCoerced = Math.Clamp(offsetOld, offsetMin, offsetMax);

            return offsetCoerced;
        }

        // Coerces the vertical offset value.
        private double CoerceVerticalOffset(double offsetOld)
        {
            // Size() doesn't allow negative values.
            // Therefore, offset value is constrained between 0 and (extent.H - viewport.H).
            var offsetMin = 0.0d;
            var offsetMax = Math.Max(offsetMin, _extent.Height - _viewport.Height);
            var offsetCoerced = Math.Clamp(offsetOld, offsetMin, offsetMax);

            return offsetCoerced;
        }

        // Vertically translates ScrollViewer background (grids) by using the provided offes value.
        private void UpdateBackgroundViewportVerticalTranslation(double value)
        {
            var minorGridViewport = _itemsOwner.MinorGridViewport;
            minorGridViewport.Y -= value;
            _itemsOwner.MinorGridViewport = minorGridViewport;

            var majorGridViewport = _itemsOwner.MajorGridViewport;
            majorGridViewport.Y -= value;
            _itemsOwner.MajorGridViewport = majorGridViewport;
        }
    }

    #endregion Methods
}
