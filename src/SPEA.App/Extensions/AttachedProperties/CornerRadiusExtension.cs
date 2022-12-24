// ==================================================================================================
// <copyright file="CornerRadiusExtension.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Extensions.AttachedProperties
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Provides an extension to set or bind corner radius.
    /// </summary>
    public static class CornerRadiusExtension
    {
        // IsValid check is performed when the actual CornerRadius value of Border is set.

        #region Attached Properties

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="GetCornerRadius(DependencyObject)"/> getter
        /// and <see cref="SetCornerRadius(DependencyObject, CornerRadius)"/> setter.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached(
                "CornerRadius",
                typeof(CornerRadius),
                typeof(CornerRadiusExtension),
                new PropertyMetadata(new CornerRadius(0)));

        #endregion Attached Properties

        #region Attached Properties Methods

        /// <summary>
        /// Gets the value of <see cref="Border.CornerRadius"/>.
        /// </summary>
        /// <param name="obj">An object the value is get from.</param>
        /// <returns>Corner radius value.</returns>
        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        /// <summary>
        /// Sets the value of <see cref="Border.CornerRadius"/>.
        /// </summary>
        /// <param name="obj">An object the value is set to.</param>
        /// <param name="value">Corner radius value.</param>
        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        #endregion Attached Properties Methods
    }
}
