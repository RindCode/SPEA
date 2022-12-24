// ==================================================================================================
// <copyright file="IndependentMarginsExtension.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Extensions.AttachedProperties
{
    using System.Windows;

    /// <summary>
    /// Provides an extension to set or bind margins independently.
    /// </summary>
    public static class IndependentMarginsExtension
    {
        // Callback call order:
        // 1. ValidateValueCallback
        // 2. CoerceValueCallback
        // 3. PropertyChangedCallback

        #region Attached Properties

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="GetMarginLeft(DependencyObject)"/> getter
        /// and <see cref="SetMarginLeft(DependencyObject, double)"/> setter.
        /// </summary>
        public static readonly DependencyProperty MarginLeftProperty =
            DependencyProperty.RegisterAttached(
                "MarginLeft",
                typeof(double),
                typeof(IndependentMarginsExtension),
                new FrameworkPropertyMetadata(
                    default(double),
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    new PropertyChangedCallback(OnMarginLeftPropertyChanged)),
                new ValidateValueCallback(IsMarginValid));

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="GetMarginTop(DependencyObject)"/> getter
        /// and <see cref="SetMarginTop(DependencyObject, double)"/> setter.
        /// </summary>
        public static readonly DependencyProperty MarginTopProperty =
            DependencyProperty.RegisterAttached(
                "MarginTop",
                typeof(double),
                typeof(IndependentMarginsExtension),
                new FrameworkPropertyMetadata(
                    default(double),
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    new PropertyChangedCallback(OnMarginTopPropertyChanged)),
                new ValidateValueCallback(IsMarginValid));

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="GetMarginRight(DependencyObject)"/> getter
        /// and <see cref="SetMarginRight(DependencyObject, double)"/> setter.
        /// </summary>
        public static readonly DependencyProperty MarginRightProperty =
            DependencyProperty.RegisterAttached(
                "MarginRight",
                typeof(double),
                typeof(IndependentMarginsExtension),
                new FrameworkPropertyMetadata(
                    default(double),
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    new PropertyChangedCallback(OnMarginRightPropertyChanged)),
                new ValidateValueCallback(IsMarginValid));

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="GetMarginBottom(DependencyObject)"/> getter
        /// and <see cref="SetMarginBottom(DependencyObject, double)"/> setter.
        /// </summary>
        public static readonly DependencyProperty MarginBottomProperty =
            DependencyProperty.RegisterAttached(
                "MarginBottom",
                typeof(double),
                typeof(IndependentMarginsExtension),
                new FrameworkPropertyMetadata(
                    default(double),
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    new PropertyChangedCallback(OnMarginBottomPropertyChanged)),
                new ValidateValueCallback(IsMarginValid));

        #endregion Attached Properties

        #region Attached Properties Methods

        /// <summary>
        /// Gets the value of the left <see cref="FrameworkElement.Margin"/>.
        /// </summary>
        /// <param name="obj">Object the value is get from.</param>
        /// <returns>Left margin value.</returns>
        public static double GetMarginLeft(DependencyObject obj)
        {
            return (double)obj.GetValue(MarginLeftProperty);
        }

        /// <summary>
        /// Sets the value of the left <see cref="FrameworkElement.Margin"/>.
        /// </summary>
        /// <param name="obj">Object the value is set to.</param>
        /// <param name="value">Left margin value.</param>
        public static void SetMarginLeft(DependencyObject obj, double value)
        {
            obj.SetValue(MarginLeftProperty, value);
        }

        /// <summary>
        /// Gets the value of the top <see cref="FrameworkElement.Margin"/>.
        /// </summary>
        /// <param name="obj">Object the value is get from.</param>
        /// <returns>Top margin value.</returns>
        public static double GetMarginTop(DependencyObject obj)
        {
            return (double)obj.GetValue(MarginTopProperty);
        }

        /// <summary>
        /// Sets the value of the top <see cref="FrameworkElement.Margin"/>.
        /// </summary>
        /// <param name="obj">Object the value is set to.</param>
        /// <param name="value">Top margin value.</param>
        public static void SetMarginTop(DependencyObject obj, double value)
        {
            obj.SetValue(MarginTopProperty, value);
        }

        /// <summary>
        /// Gets the value of the right <see cref="FrameworkElement.Margin"/>.
        /// </summary>
        /// <param name="obj">Object the value is get from.</param>
        /// <returns>Right margin value.</returns>
        public static double GetMarginRight(DependencyObject obj)
        {
            return (double)obj.GetValue(MarginRightProperty);
        }

        /// <summary>
        /// Sets the value of the right <see cref="FrameworkElement.Margin"/>.
        /// </summary>
        /// <param name="obj">Object the value is set to.</param>
        /// <param name="value">Right margin value.</param>
        public static void SetMarginRight(DependencyObject obj, double value)
        {
            obj.SetValue(MarginRightProperty, value);
        }

        /// <summary>
        /// Gets the value of the bottom <see cref="FrameworkElement.Margin"/>.
        /// </summary>
        /// <param name="obj">Object the value is get from.</param>
        /// <returns>Bottom margin value.</returns>
        public static double GetMarginBottom(DependencyObject obj)
        {
            return (double)obj.GetValue(MarginBottomProperty);
        }

        /// <summary>
        /// Sets the value of the bottom <see cref="FrameworkElement.Margin"/>.
        /// </summary>
        /// <param name="obj">Object the value is set to.</param>
        /// <param name="value">Bottom margin value.</param>
        public static void SetMarginBottom(DependencyObject obj, double value)
        {
            obj.SetValue(MarginBottomProperty, value);
        }

        #endregion Attached Properties Methods

        #region Attached Properties Callbacks

        // Is called every time the margin value is changed to update the actual Margin property.
        private static void OnMarginLeftPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = d as FrameworkElement;
            if (element != null)
            {
                // Marin as a dp (Thickness), so we can set it only as a whole.
                var margin = element.Margin;
                margin.Left = (double)e.NewValue;
                element.Margin = margin;
            }
        }

        // Is called every time the margin value is changed to update the actual Margin property.
        private static void OnMarginTopPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = d as FrameworkElement;
            if (element != null)
            {
                // Marin as a dp (Thickness), so we can set it only as a whole.
                var margin = element.Margin;
                margin.Top = (double)e.NewValue;
                element.Margin = margin;
            }
        }

        // Is called every time the margin value is changed to update the actual Margin property.
        private static void OnMarginRightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = d as FrameworkElement;
            if (element != null)
            {
                // Marin as a dp (Thickness), so we can set it only as a whole.
                var margin = element.Margin;
                margin.Right = (double)e.NewValue;
                element.Margin = margin;
            }
        }

        // Is called every time the margin value is changed to update the actual Margin property.
        private static void OnMarginBottomPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = d as FrameworkElement;
            if (element != null)
            {
                // Marin as a dp (Thickness), so we can set it only as a whole.
                var margin = element.Margin;
                margin.Bottom = (double)e.NewValue;
                element.Margin = margin;
            }
        }

        #endregion Attached Properties Callbacks

        #region Validation Methods

        // Checks if margin value is valid before setting it.
        private static bool IsMarginValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            var m = (double)value;

            // Margin IsValid check for Border class allows negative values and positive infinite values,
            // while NaN and negative infinite values are not allowed (why is negative infinite not allowed?).
            if (double.IsNaN(m) || double.IsNegativeInfinity(m))
            {
                return false;
            }

            return true;
        }

        #endregion Validation Methods
    }
}
