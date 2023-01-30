// ==================================================================================================
// <copyright file="SElementContainer.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Controls.SViewport
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Represents a UI container (basically - a bounding box) for an element
    /// located inside of <see cref="SViewportItemsHostControl"/>.
    /// </summary>
    public class SElementContainer : ContentControl
    {
        #region Dependency Properties

        /// <summary>
        /// DependencyProperty for <see cref="Left"/> property.
        /// </summary>
        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register(
                "Left",
                typeof(double),
                typeof(SElementContainer),
                new PropertyMetadata(0.0d));

        /// <summary>
        /// Gets or sets a location of the left bound for the current <see cref="SElementContainer"/>
        /// in <see cref="SViewportControl.ItemsHost"/> coordinates.
        /// </summary>
        public double Left
        {
            get { return (double)GetValue(BoundingBoxProperty); }
            set { SetValue(BoundingBoxProperty, value); }
        }

        /// <summary>
        /// DependencyProperty for <see cref="Top"/> property.
        /// </summary>
        public static readonly DependencyProperty TopProperty =
            DependencyProperty.Register(
                "Top",
                typeof(double),
                typeof(SElementContainer),
                new PropertyMetadata(0.0d));

        /// <summary>
        /// Gets or sets a location of the top bound for the current <see cref="SElementContainer"/>
        /// in <see cref="SViewportControl.ItemsHost"/> coordinates.
        /// </summary>
        public double Top
        {
            get { return (double)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        /// <summary>
        /// DependencyProperty for <see cref="Right"/> property.
        /// </summary>
        public static readonly DependencyProperty RightProperty =
            DependencyProperty.Register(
                "Right",
                typeof(double),
                typeof(SElementContainer),
                new PropertyMetadata(0.0d));

        /// <summary>
        /// Gets or sets a location of the right bound for the current <see cref="SElementContainer"/>
        /// in <see cref="SViewportControl.ItemsHost"/> coordinates.
        /// </summary>
        public double Right
        {
            get { return (double)GetValue(RightProperty); }
            set { SetValue(RightProperty, value); }
        }

        /// <summary>
        /// DependencyProperty for <see cref="Bottom"/> property.
        /// </summary>
        public static readonly DependencyProperty BottomProperty =
            DependencyProperty.Register(
                "Bottom",
                typeof(double),
                typeof(SElementContainer),
                new PropertyMetadata(0.0d));

        /// <summary>
        /// Gets or sets a location of the bottom bound for the current <see cref="SElementContainer"/>
        /// in <see cref="SViewportControl.ItemsHost"/> coordinates.
        /// </summary>
        public double Bottom
        {
            get { return (double)GetValue(BottomProperty); }
            set { SetValue(BottomProperty, value); }
        }

        /// <summary>
        /// DependencyProperty for <see cref="BoundingBox"/> property.
        /// </summary>
        public static readonly DependencyProperty BoundingBoxProperty =
            DependencyProperty.Register(
                "BoundingBox",
                typeof(Rect),
                typeof(SElementContainer),
                new PropertyMetadata(Rect.Empty));

        /// <summary>
        /// Gets or sets a bounding box for the current <see cref="SElementContainer"/>.
        /// </summary>
        public Rect BoundingBox
        {
            get { return (Rect)GetValue(BoundingBoxProperty); }
            set { SetValue(BoundingBoxProperty, value); }
        }

        #endregion Dependency Properties

        #region Properties

        #endregion Properties
    }
}
