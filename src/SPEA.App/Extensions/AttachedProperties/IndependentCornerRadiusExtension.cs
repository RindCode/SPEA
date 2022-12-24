// ==================================================================================================
// <copyright file="IndependentCornerRadiusExtension.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Extensions.AttachedProperties
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Provides an extension to set or bind corner radius independently.
    /// </summary>
    public static class IndependentCornerRadiusExtension
    {
        // Callback call order:
        // 1. ValidateValueCallback
        // 2. CoerceValueCallback
        // 3. PropertyChangedCallback

        #region Attached Properties

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="GetCornerRadiusTopLeft(DependencyObject)"/> getter
        /// and <see cref="SetCornerRadiusTopLeft(DependencyObject, double)"/> setter.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusTopLeftProperty =
            DependencyProperty.RegisterAttached(
                "CornerRadiusTopLeft",
                typeof(double),
                typeof(IndependentCornerRadiusExtension),
                new FrameworkPropertyMetadata(
                    default(double),
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    new PropertyChangedCallback(OnCornerRadiusTopLeftPropertyChanged)),
                new ValidateValueCallback(IsCornerRadiusValid));

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="GetCornerRadiusTopRight(DependencyObject)"/> getter
        /// and <see cref="SetCornerRadiusTopRight(DependencyObject, double)"/> setter.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusTopRightProperty =
            DependencyProperty.RegisterAttached(
                "CornerRadiusTopRight",
                typeof(double),
                typeof(IndependentCornerRadiusExtension),
                new FrameworkPropertyMetadata(
                    default(double),
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    new PropertyChangedCallback(OnCornerRadiusTopRightPropertyChanged)),
                new ValidateValueCallback(IsCornerRadiusValid));

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="GetCornerRadiusBottomRight(DependencyObject)"/> getter
        /// and <see cref="SetCornerRadiusBottomRight(DependencyObject, double)"/> setter.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusBottomRightProperty =
            DependencyProperty.RegisterAttached(
                "CornerRadiusBottomRight",
                typeof(double),
                typeof(IndependentCornerRadiusExtension),
                new FrameworkPropertyMetadata(
                    default(double),
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    new PropertyChangedCallback(OnCornerRadiusBottomRightPropertyChanged)),
                new ValidateValueCallback(IsCornerRadiusValid));

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="GetCornerRadiusBottomLeft(DependencyObject)"/> getter
        /// and <see cref="SetCornerRadiusTopLeft(DependencyObject, double)"/> setter.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusBottomLeftProperty =
            DependencyProperty.RegisterAttached(
                "CornerRadiusBottomLeft",
                typeof(double),
                typeof(IndependentCornerRadiusExtension),
                new FrameworkPropertyMetadata(
                    default(double),
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    new PropertyChangedCallback(OnCornerRadiusBottomLeftPropertyChanged)),
                new ValidateValueCallback(IsCornerRadiusValid));

        #endregion Attached Properties

        #region Attached Properties Methods

        /// <summary>
        /// Gets the value of the top left <see cref="Border.CornerRadius"/>.
        /// </summary>
        /// <param name="obj">Object the value is get from.</param>
        /// <returns>Top left corner radius value.</returns>
        public static double GetCornerRadiusTopLeft(DependencyObject obj)
        {
            return (double)obj.GetValue(CornerRadiusTopLeftProperty);
        }

        /// <summary>
        /// Sets the value of the top left <see cref="Border.CornerRadius"/>.
        /// </summary>
        /// <param name="obj">Object the value is set to.</param>
        /// <param name="value">Top left corner radius value.</param>
        public static void SetCornerRadiusTopLeft(DependencyObject obj, double value)
        {
            obj.SetValue(CornerRadiusTopLeftProperty, value);
        }

        /// <summary>
        /// Gets the value of the top right <see cref="Border.CornerRadius"/>.
        /// </summary>
        /// <param name="obj">Object the value is get from.</param>
        /// <returns>Top right corner radius value.</returns>
        public static double GetCornerRadiusTopRight(DependencyObject obj)
        {
            return (double)obj.GetValue(CornerRadiusTopRightProperty);
        }

        /// <summary>
        /// Sets the value of the top right <see cref="Border.CornerRadius"/>.
        /// </summary>
        /// <param name="obj">Object the value is set to.</param>
        /// <param name="value">Top right corner radius value.</param>
        public static void SetCornerRadiusTopRight(DependencyObject obj, double value)
        {
            obj.SetValue(CornerRadiusTopRightProperty, value);
        }

        /// <summary>
        /// Gets the value of the bottom right <see cref="Border.CornerRadius"/>.
        /// </summary>
        /// <param name="obj">Object the value is get from.</param>
        /// <returns>Bottom right corner radius value.</returns>
        public static double GetCornerRadiusBottomRight(DependencyObject obj)
        {
            return (double)obj.GetValue(CornerRadiusBottomRightProperty);
        }

        /// <summary>
        /// Sets the value of the bottom right <see cref="Border.CornerRadius"/>.
        /// </summary>
        /// <param name="obj">Object the value is set to.</param>
        /// <param name="value">Bottom right corner radius value.</param>
        public static void SetCornerRadiusBottomRight(DependencyObject obj, double value)
        {
            obj.SetValue(CornerRadiusBottomRightProperty, value);
        }

        /// <summary>
        /// Gets the value of the bottom left <see cref="Border.CornerRadius"/>.
        /// </summary>
        /// <param name="obj">Object the value is get from.</param>
        /// <returns>Bottom left corner radius value.</returns>
        public static double GetCornerRadiusBottomLeft(DependencyObject obj)
        {
            return (double)obj.GetValue(CornerRadiusBottomLeftProperty);
        }

        /// <summary>
        /// Sets the value of the bottom left <see cref="Border.CornerRadius"/>.
        /// </summary>
        /// <param name="obj">Object the value is set to.</param>
        /// <param name="value">Bottom left corner radius value.</param>
        public static void SetCornerRadiusBottomLeft(DependencyObject obj, double value)
        {
            obj.SetValue(CornerRadiusBottomLeftProperty, value);
        }

        #endregion Attached Properties Methods

        #region Attached Properties Callbacks

        // Is called every time the corner radius value is changed to update the actual CornerRadius property.
        private static void OnCornerRadiusTopLeftPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Border element = d as Border;
            if (element != null)
            {
                // Marin as a dp (Thickness), so we can set it only as a whole.
                var cr = element.CornerRadius;
                cr.TopLeft = (double)e.NewValue;
                element.CornerRadius = cr;
            }
        }

        // Is called every time the corner radius value is changed to update the actual CornerRadius property.
        private static void OnCornerRadiusTopRightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Border element = d as Border;
            if (element != null)
            {
                // Marin as a dp (Thickness), so we can set it only as a whole.
                var cr = element.CornerRadius;
                cr.TopRight = (double)e.NewValue;
                element.CornerRadius = cr;
            }
        }

        // Is called every time the corner radius value is changed to update the actual CornerRadius property.
        private static void OnCornerRadiusBottomRightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Border element = d as Border;
            if (element != null)
            {
                // Marin as a dp (Thickness), so we can set it only as a whole.
                var cr = element.CornerRadius;
                cr.BottomRight = (double)e.NewValue;
                element.CornerRadius = cr;
            }
        }

        // Is called every time the corner radius value is changed to update the actual CornerRadius property.
        private static void OnCornerRadiusBottomLeftPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Border element = d as Border;
            if (element != null)
            {
                // Marin as a dp (Thickness), so we can set it only as a whole.
                var cr = element.CornerRadius;
                cr.BottomLeft = (double)e.NewValue;
                element.CornerRadius = cr;
            }
        }

        #endregion Attached Properties Callbacks

        #region Validation Methods

        // Checks if corner radius value is valid before setting it.
        private static bool IsCornerRadiusValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            var cr = (double)value;

            // CornerRadius IsValid check for Border class does not allow negative values, NaN and any kind of Infinity.
            if (double.IsNegative(cr) || double.IsNaN(cr) || double.IsNegativeInfinity(cr) || double.IsNegativeInfinity(cr))
            {
                return false;
            }

            return true;
        }

        #endregion Validation Methods
    }
}
