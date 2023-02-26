// ==================================================================================================
// <copyright file="SPopup.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Controls
{
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using SPEA.App.Utils.Helpers;

    /// <summary>
    /// Represents an extended <see cref="Popup"/> control.
    /// </summary>
    public class SPopup : Popup
    {
        #region Fields

        private Window _parentWindow;
        private bool _safeCloseBoundariesLocked = false;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SPopup"/> class.
        /// </summary>
        public SPopup()
        {
            Loaded += SPopup_Loaded;
            Unloaded += SPopup_Unloaded;
        }

        #endregion Constructors

        #region Dependency Properties

        /// <summary>
        /// DependencyProperty for <see cref="EnableSafeCloseBoundaries"/> property.
        /// </summary>
        public static readonly DependencyProperty EnableSafeCloseBoundariesProperty =
            DependencyProperty.Register(
                "EnableSafeCloseBoundaries",
                typeof(bool),
                typeof(SPopup),
                new PropertyMetadata(true));

        /// <summary>
        /// Gets or sets a value indicating whether the popup is prevented from closing
        /// if the mouse click is done within some safe area around the popup.
        /// </summary>
        public bool EnableSafeCloseBoundaries
        {
            get { return (bool)GetValue(EnableSafeCloseBoundariesProperty); }
            set { SetValue(EnableSafeCloseBoundariesProperty, value); }
        }

        /// <summary>
        /// DependencyProperty for <see cref="SafeCloseDistance"/> property.
        /// </summary>
        public static readonly DependencyProperty SafeCloseDistanceProperty =
            DependencyProperty.Register(
                "SafeCloseDistance",
                typeof(double),
                typeof(SPopup),
                new PropertyMetadata(
                    50d,
                    new PropertyChangedCallback(OnSafeCloseDistanceChanged),
                    new CoerceValueCallback(CoerceSafeCloseDistance)));

        /// <summary>
        /// Gets or sets the distance from the popup border to the edges of a virtual
        /// rectangle around the popup. The popup will not be closed if clicked inside this
        /// safe area with <see cref="EnableSafeCloseBoundaries"/> dependency property
        /// set to <see langword="true"/>.
        /// </summary>
        public double SafeCloseDistance
        {
            get { return (double)GetValue(SafeCloseDistanceProperty); }
            set { SetValue(SafeCloseDistanceProperty, value); }
        }

        // SafeCloseDistanceProperty PropertyChanged callback.
        private static void OnSafeCloseDistanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(SafeCloseDistanceProperty);
        }

        // SafeCloseDistanceProperty CoerceValue callback.
        private static object CoerceSafeCloseDistance(DependencyObject d, object value)
        {
            double currValue = (double)value;
            currValue = currValue < 0 ? 0 : currValue;
            return currValue;
        }

        #endregion Dependency Properties

        #region Properties

        /// <summary>
        /// Gets a value indicating whether safe close area is "locked", which means the popup
        /// will not be closed even with <see cref="EnableSafeCloseBoundaries"/> set to
        /// <see langword="true"/> and mouse pointer being outside of thos area.
        /// </summary>
        /// <remarks>
        /// Is used in a situation, when <see cref="EnableSafeCloseBoundaries"/> is
        /// <see langword="true"/>, but <see cref="SafeCloseDistance"/> is lower than
        /// the distance to the pointer.
        /// </remarks>
        internal bool SafeCloseBoundariesLocked
        {
            get => _safeCloseBoundariesLocked;
            private set => _safeCloseBoundariesLocked = value;
        }

        #endregion Properties

        #region Methods

        /// <inheritdoc/>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            // Fixes the problem when popup closes on left mouse button click inside its area.

            base.OnMouseLeftButtonDown(e);
            e.Handled = true;
        }

        /// <inheritdoc/>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            SafeCloseBoundariesLocked = true;
        }

        /// <inheritdoc/>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            SafeCloseBoundariesLocked = false;
        }

        /// <inheritdoc/>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            OnMouseMove_HandleSafeClose(e);

            e.Handled = true;
        }

        // Handles safe close on MouseMove event.
        private void OnMouseMove_HandleSafeClose(MouseEventArgs e)
        {
            // If popup is already open, but safe close is not requested.
            // Also we do nothing if there is no child UIElement.
            if (!IsOpen || !EnableSafeCloseBoundaries || Child == null)
            {
                return;
            }

            var p = e.GetPosition(Child);
            var isInside = IsInsideSafeCloseBoundaries(p);

            // Handles the case when we enter safe close area for the first time,
            // after being outside of it on the popup opening.
            if (SafeCloseBoundariesLocked && isInside)
            {
                SafeCloseBoundariesLocked = false;
                return;
            }

            // Normal behaviour on leaving safe close boundaries.
            if (!isInside && !SafeCloseBoundariesLocked)
            {
                IsOpen = false;
            }
        }

        // Handles Loaded event.
        private void SPopup_Loaded(object sender, RoutedEventArgs e)
        {
            var parentWindow = VisualTreeHelperEx.FindParent<Window>(this);
            if (parentWindow != null)
            {
                _parentWindow = parentWindow;
                _parentWindow.LocationChanged += ParentWindow_LocationChanged;
            }
        }

        // Handles Unloaded event.
        private void SPopup_Unloaded(object sender, RoutedEventArgs e)
        {
            Loaded += SPopup_Loaded;
            Unloaded -= SPopup_Unloaded;
            _parentWindow.LocationChanged -= ParentWindow_LocationChanged;
        }

        // Handles LocationChanged events of a parent window.
        private void ParentWindow_LocationChanged(object sender, System.EventArgs e)
        {
            // This is a workaround to keep the PopUp position at the same place
            // when the parent Window is moved or resized. WPF Popup Reposition() and UpdatePosition()
            // methods are implemented as internal and private, so we cannot call them directly.
            // However, HorizontalOffset, VerticalOffset and PlacementRectangle DPs have OnOffsetChanged()
            // callback, which in turn will call Reposition() method

            var oldValue = VerticalOffset;
            VerticalOffset -= 0.001;
            VerticalOffset = oldValue;
        }

        // Indicates whether the given mouse position in popup's coordinates is inside
        // a virtual "safe close" rectangle located around the control.
        private bool IsInsideSafeCloseBoundaries(Point position)
        {
            var width = Child.RenderSize.Width;
            var height = Child.RenderSize.Height;
            var d = SafeCloseDistance;

            return position.X - width <= d && position.X >= -d && position.Y - height <= d && position.Y >= -d;
        }

        #endregion Methods
    }
}
