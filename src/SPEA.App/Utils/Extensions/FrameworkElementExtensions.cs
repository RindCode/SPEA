// ==================================================================================================
// <copyright file="FrameworkElementExtensions.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Utils.Extensions
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Provides access to various <see cref="FrameworkElement"/> extension methods.
    /// </summary>
    public static class FrameworkElementExtensions
    {
        #region Methods

        /// <summary>
        /// Calculates the bounding box for a child element relatively to the visual parent based on applied transforms.
        /// </summary>
        /// <param name="element">Element the bounding box will be calculated for.</param>
        /// <param name="from">Visual element transforms will be calculated from.</param>
        /// <returns><see cref="Rect"/> bounding box.</returns>
        public static Rect GetBoundingBox(this FrameworkElement element, Visual from)
        {
            if (from == null)
            {
                throw new ArgumentNullException(nameof(from));
            }

            var transform = element.TransformToVisual(from);

            // W and H DP default values are NaN.
            if (double.IsNaN(element.Width) || double.IsNaN(element.Width))
            {
                var actualBounds = transform.TransformBounds(new Rect(0, 0, element.ActualWidth, element.ActualHeight));
                return actualBounds;
            }

            var bounds = transform.TransformBounds(new Rect(0, 0, element.Width, element.Height));
            return bounds;
        }

        #endregion Methods
    }
}
