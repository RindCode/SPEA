// ==================================================================================================
// <copyright file="SViewportItemsHostControl.cs" company="Dmitry Poberezhnyy">
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
    using SPEA.App.Utils.Extensions;

    /// <summary>
    /// Represents items host for <see cref="SElementItemContainer"/> objects.
    /// </summary>
    public class SViewportItemsHostControl : Panel
    {
        /// <summary>
        /// Gets or sets the parent <see cref="ItemsControl"/>.
        /// </summary>
        public SViewportControl? ItemsOwner { get; set; }

        /// <summary>
        /// Gets or sets the left most element.
        /// </summary>
        public SElementItemContainer? LeftMostElement { get; set; }

        /// <summary>
        /// Gets or sets the top most element.
        /// </summary>
        public SElementItemContainer? TopMostElement { get; set; }

        /// <summary>
        /// Gets or sets the right most element.
        /// </summary>
        public SElementItemContainer? RightMostElement { get; set; }

        /// <summary>
        /// Gets or sets the bottom most element.
        /// </summary>
        public SElementItemContainer? BottomMostElement { get; set; }

        /// <inheritdoc/>
        protected override Size MeasureOverride(Size constraint)
        {
            Debug.WriteLine($"ITEMSHOST MEASURE OVERRIDE = {constraint}");

            foreach (UIElement child in InternalChildren)
            {
                var container = child as SElementItemContainer;
                container?.Measure(constraint);
            }

            return new Size(0, 0);
        }

        /// <inheritdoc/>
        protected override Size ArrangeOverride(Size finalSize)
        {
            Debug.WriteLine($"ITEMSHOST ARRANGE OVERRIDE = {finalSize}");

            var minX = double.MaxValue;
            var minY = double.MaxValue;
            var maxX = double.MinValue;
            var maxY = double.MinValue;

            foreach (UIElement child in InternalChildren)
            {
                if (child is SElementItemContainer container)
                {
                    container.BoundingBox = container.GetBoundingBox(this);  // extension method

                    child.Arrange(new Rect(new Point(0, 0), container.DesiredSize));

                    ////if (!container.IsValid)
                    ////{
                    ////    continue;
                    ////}

                    minX = Math.Min(minX, container.BoundingBox.Left);
                    minY = Math.Min(minY, container.BoundingBox.Top);
                    maxX = Math.Max(maxX, container.BoundingBox.Right);
                    maxY = Math.Max(maxY, container.BoundingBox.Bottom);

                    if (container.BoundingBox.Left <= minX)
                    {
                        LeftMostElement = container;
                    }

                    if (container.BoundingBox.Top <= minY)
                    {
                        TopMostElement = container;
                    }

                    if (container.BoundingBox.Right >= maxX)
                    {
                        RightMostElement = container;
                    }

                    if (container.BoundingBox.Bottom >= maxY)
                    {
                        BottomMostElement = container;
                    }

                    Debug.WriteLine($"bounds: left={container.BoundingBox.Left,8:F3}, top={container.BoundingBox.Top,8:F3}, right={container.BoundingBox.Right,8:F3}, bottom={container.BoundingBox.Bottom,8:F3}");
                }
            }

            return finalSize;
        }
    }
}
