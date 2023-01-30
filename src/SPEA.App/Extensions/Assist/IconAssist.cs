// ==================================================================================================
// <copyright file="IconAssist.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Extensions.Assist
{
    using System.Windows;
    using System.Windows.Shapes;

    /// <summary>
    /// Provides various attached properties for controls icon(s).
    /// </summary>
    public static class IconAssist
    {
        #region Attached Properties

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="GetIcon(DependencyObject)"/> getter
        /// and <see cref="SetIcon(DependencyObject, Path)"/> setter.
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.RegisterAttached(
                "Icon",
                typeof(Path),
                typeof(IconAssist),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets the value of <see cref="IconProperty"/>.
        /// </summary>
        /// <param name="obj">An object the value is get from.</param>
        /// <returns>Icon content.</returns>
        public static Path GetIcon(DependencyObject obj)
        {
            return (Path)obj.GetValue(IconProperty);
        }

        /// <summary>
        /// Sets the value of <see cref="IconProperty"/>.
        /// </summary>
        /// <param name="obj">An object the value is set to.</param>
        /// <param name="value">Icon content.</param>
        public static void SetIcon(DependencyObject obj, Path value)
        {
            obj.SetValue(IconProperty, value);
        }

        #endregion Attached Properties
    }
}
