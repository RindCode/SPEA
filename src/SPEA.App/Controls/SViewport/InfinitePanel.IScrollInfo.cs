// ==================================================================================================
// <copyright file="InfinitePanel.IScrollInfo.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Controls.SViewport
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

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
    public partial class InfinitePanel : IScrollInfo
    {
        #region Properties

        /// <inheritdoc/>
        public bool CanHorizontallyScroll { get; set; }

        /// <inheritdoc/>
        public bool CanVerticallyScroll { get; set; }

        /// <inheritdoc/>
        public double ExtentHeight => ContentScale * _extent.Height;

        /// <inheritdoc/>
        public double ExtentWidth => ContentScale * _extent.Width;

        /// <inheritdoc/>
        public double HorizontalOffset => ContentScale * _offset.Width;

        /// <inheritdoc/>
        public double VerticalOffset => ContentScale * _offset.Height;

        /// <inheritdoc/>
        public double ViewportHeight => _viewport.Height;

        /// <inheritdoc/>
        public double ViewportWidth => _viewport.Width;

        /// <inheritdoc/>
        public ScrollViewer? ScrollOwner { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc/>
        public void LineDown()
        {
            TranslateVertically(ScrollFactor * _viewport.Height);
        }

        /// <inheritdoc/>
        public void LineLeft()
        {
            TranslateHorizontally(ScrollFactor * -_viewport.Width);
        }

        /// <inheritdoc/>
        public void LineRight()
        {
            TranslateHorizontally(ScrollFactor * _viewport.Width);
        }

        /// <inheritdoc/>
        public void LineUp()
        {
            TranslateVertically(ScrollFactor * -_viewport.Height);
        }

        /// <inheritdoc/>
        public Rect MakeVisible(Visual visual, Rect rectangle)
        {
            return default(Rect);
        }

        /// <inheritdoc/>
        public void MouseWheelDown()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public void MouseWheelLeft()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public void MouseWheelRight()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public void MouseWheelUp()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public void PageDown()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public void PageLeft()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public void PageRight()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public void PageUp()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public void SetHorizontalOffset(double offset)
        {
            _offset.Width = offset;
        }

        /// <inheritdoc/>
        public void SetVerticalOffset(double offset)
        {
            _offset.Height = offset;
        }

        #endregion Methods
    }
}
