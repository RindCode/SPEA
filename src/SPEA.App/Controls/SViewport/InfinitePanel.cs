// ==================================================================================================
// <copyright file="InfinitePanel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Controls.SViewport
{
    using System;
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
    /// <see cref="SViewportItemsHostControl"/> (which in turn, acts as a host for <see cref="SElementContainer"/> objects).
    /// It must be wrapped by <see cref="ScrollViewer"/> inside <see cref="SViewportControl"/>
    /// ControlTemplate with <see cref="ScrollViewer.CanContentScroll"/> property set to <see langword="true"/>.
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

        private readonly TranslateTransform _translateTransform = new TranslateTransform();
        private readonly ScaleTransform _scaleTransform = new ScaleTransform();

        private SViewportControl _parent = null;
        private FrameworkElement _content = null;
        private Size _extent = new Size(0, 0);
        private Size _offset = new Size(0, 0);
        private Size _viewport = new Size(0, 0);
        private double _scrollFactor = 0.05d;
        private double _zoomFactor = 1.0d;

        // Scrollbars references to handle their events. Their PART_ names are defined within ControlTemplate.
        // TODO: kinda bad workaround.
        ////private ScrollBar _horizontalScrollBar = null;
        ////private ScrollBar _verticalScrollBar = null;

        #endregion Fields

        #region Properties

        #endregion Properties

        #region Methods

        /// <summary>
        /// Scrolls and translates the <see cref="ScrollOwner"/> viewport in a horizontal direction.
        /// </summary>
        /// <param name="value">A value the content will be scrolled by.</param>
        public void TranslateHorizontally(double value)
        {
            if (_parent == null)
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
            var contentBounds = _content.GetBoundingBox(_parent);  // extension method
            var isOutsideContent = _extent.Width > contentBounds.X + contentBounds.Width;
            if (contentBounds.X > 0 || isOutsideContent)
            {
                _extent.Width -= Math.Abs(value);
            }

            // If we remain inside the extent borders, it will be adjusted (reduced) until
            // it reaches the content bounding box, but never smaller than that.
            ////var contentBounds = GetBoundsRelativeTo(_content, _parent);
            ////var isInsideContentBounds = contentBounds.X - calculatedOffset < 0;
            ////if (contentBounds.X > 0 && isInsideContentBounds)
            ////{
            ////    _extent.Width -= Math.Abs(contentBounds.X - calculatedOffset);
            ////}

            var coercedOffset = CoerceHorizontalOffset(calculatedOffset);
            SetHorizontalOffset(coercedOffset);

            UpdateContentHorizontalTranslation(value);
            UpdateBackgroundViewportHorizontalTranslation(value);

            ScrollOwner?.InvalidateScrollInfo();
        }

        /// <summary>
        /// Scrolls and translates the <see cref="ScrollOwner"/> viewport in a vertical direction.
        /// </summary>
        /// <param name="valueY">A value the content will be scrolled by.</param>
        public void TranslateVertically(double valueY)
        {
            if (_parent == null)
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
            base.OnApplyTemplate();

            _content = _parent.Template.FindName(ItemsHostPartName, _parent) as FrameworkElement;
            if (_content == null)
            {
                throw new NullReferenceException($"Couldn't find the element with the following PART_ name: {ItemsHostPartName}");
            }

            ////_horizontalScrollBar = ScrollOwner.Template.FindName(HorizontalScrollBarPartName, ScrollOwner) as ScrollBar;
            ////if (_content == null)
            ////{
            ////    throw new NullReferenceException($"Couldn't find the element with the following PART_ name: {HorizontalScrollBarPartName}");
            ////}

            ////_verticalScrollBar = ScrollOwner.Template.FindName(VerticalScrollBarPartName, ScrollOwner) as ScrollBar;
            ////if (_content == null)
            ////{
            ////    throw new NullReferenceException($"Couldn't find the element with the following PART_ name: {VerticalScrollBarPartName}");
            ////}

            var trasnformGroup = new TransformGroup();
            trasnformGroup.Children.Add(_translateTransform);
            trasnformGroup.Children.Add(_scaleTransform);
            _content.RenderTransform = trasnformGroup;

            Unloaded += InfinitePanel_Unloaded;
            ////_horizontalScrollBar.Scroll += HorizontalScrollBar_Scroll;
        }

        /// <summary>
        /// Internal initializer. Normally called from SViewportControl.
        /// </summary>
        /// <param name="obj">A reference to a parent control.</param>
        internal void Initialize(SViewportControl obj)
        {
            _parent = obj;
        }

        /// <inheritdoc/>
        protected override Size MeasureOverride(Size constraint)
        {
            // Current control naturally provides the infinite available space to its children,
            // since it's wrapped by ScrollViewer.
            var infiniteSize = new Size(double.PositiveInfinity, double.PositiveInfinity);

            // The default behavior of the Content/ContentControl MeasureOverride is
            // to measure only the first visual child (in this case - Content element).
            var contentSize = base.MeasureOverride(infiniteSize);

            // Make sure we give enough space requested by the child element.
            if (contentSize != _extent)
            {
                _extent = contentSize;
                ScrollOwner?.InvalidateScrollInfo();
            }

            // Update viewport size based on constraint provided by ScrollViewer.
            UpdateViewportSize(constraint);

            // Returning PositiveInfinity will raise InvalidOperationException, thus not allowed.
            // Just use child dimensions to calculate the desired size in such case.

            var width = constraint.Width;
            if (width == double.PositiveInfinity)
            {
                width = contentSize.Width;
            }

            var height = constraint.Height;
            if (height == double.PositiveInfinity)
            {
                height = contentSize.Height;
            }

            var desiredSize = new Size(width, height);

            return desiredSize;
        }

        /// <inheritdoc/>
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            // The default behavior of the Content/ContentControl ArrangeOverride is
            // to measure only the first visual child (in this case - Content element)
            // without any transforms to be applied.
            Size actualRenderSize = base.ArrangeOverride(arrangeBounds);

            // Make sure we give enough space requested by the child element.
            // The content's desired size is computed beforehand in base.MeasureOverride(...) call.
            if (_content.DesiredSize != _extent)
            {
                _extent = _content.DesiredSize;
                ScrollOwner?.InvalidateScrollInfo();
            }

            // Update viewport size based on constraint provided by ScrollViewer.
            UpdateViewportSize(arrangeBounds);

            return actualRenderSize;
        }

        //
        private static Rect GetBoundsRelativeTo(FrameworkElement child, Visual parent)
        {
            return child.TransformToVisual(parent).TransformBounds(new Rect(child.RenderSize));
        }

        // Handles stuff before the element is removed from the element tree.
        private void InfinitePanel_Unloaded(object sender, RoutedEventArgs e)
        {
            Unloaded -= InfinitePanel_Unloaded;
            ////_horizontalScrollBar.Scroll -= HorizontalScrollBar_Scroll;
        }

        // 
        private void HorizontalScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            var oldOffset = _offset.Width;
            var newOffset = e.NewValue;
            var delta = newOffset - oldOffset;

            TranslateHorizontally(delta);
        }

        // Updates the viewport size.
        private void UpdateViewportSize(Size newSize)
        {
            if (_viewport == newSize)
            {
                return;
            }

            _viewport = newSize;

            ScrollOwner?.InvalidateScrollInfo();
        }

        // Translates the horizontal translation by using the provided offes value.
        private void UpdateContentHorizontalTranslation(double value)
        {
            if (_translateTransform != null)
            {
                _translateTransform.X += -value;
            }
        }

        // Horizontally translates ScrollViewer background (grids) by using the provided offes value.
        private void UpdateBackgroundViewportHorizontalTranslation(double value)
        {
            var minorGridViewport = _parent.MinorGridViewport;
            minorGridViewport.X -= value;
            _parent.MinorGridViewport = minorGridViewport;

            var majorGridViewport = _parent.MajorGridViewport;
            majorGridViewport.X -= value;
            _parent.MajorGridViewport = majorGridViewport;
        }

        // Coerces the horizontal offset value.
        private double CoerceHorizontalOffset(double offsetOld)
        {
            // Size() doesn't allow negative values.
            // Offset value is constrained between 0 and (extent.Width - viewport.Width).
            var offsetMin = 0.0d;
            var offsetMax = Math.Max(offsetMin, _extent.Width - _viewport.Width);
            var offsetCoerced = Math.Clamp(offsetOld, offsetMin, offsetMax);

            return offsetCoerced;
        }

        // Updates the vertical translation by using the provided offes value.
        private void UpdateVerticalTranslation(double value)
        {
            if (_translateTransform != null)
            {
                _translateTransform.Y += -value;
            }
        }

        // Coerces the vertical offset value.
        private double CoerceVerticalOffset(double offsetOld)
        {
            // Size() doesn't allow negative values.
            // Therefore, offset value is constrained between 0 and (extent.Height - viewport.Height).
            var offsetMin = 0.0d;
            var offsetMax = Math.Max(offsetMin, _extent.Height - _viewport.Height);
            var offsetCoerced = Math.Clamp(offsetOld, offsetMin, offsetMax);

            return offsetCoerced;
        }

        // Vertically translates ScrollViewer background (grids) by using the provided offes value.
        private void UpdateBackgroundViewportVerticalTranslation(double value)
        {
            var minorGridViewport = _parent.MinorGridViewport;
            minorGridViewport.Y -= value;
            _parent.MinorGridViewport = minorGridViewport;

            var majorGridViewport = _parent.MajorGridViewport;
            majorGridViewport.Y -= value;
            _parent.MajorGridViewport = majorGridViewport;
        }
    }

    #endregion Methods
}
