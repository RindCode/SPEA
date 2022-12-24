// ==================================================================================================
// <copyright file="SectionElementContainer.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Controls.SectionEditor
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Represents a UI container (basically - a bounding box) for a cross-section element.
    /// </summary>
    public class SectionElementContainer : ContentControl
    {
        #region Dependency Properties

        /// <summary>
        /// DependencyProperty for <see cref="Left"/> property.
        /// </summary>
        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register(
                "Left",
                typeof(double),
                typeof(SectionElementContainer),
                new PropertyMetadata(0.0d));

        /// <summary>
        /// DependencyProperty for <see cref="Top"/> property.
        /// </summary>
        public static readonly DependencyProperty TopProperty =
            DependencyProperty.Register(
                "Top",
                typeof(double),
                typeof(SectionElementContainer),
                new PropertyMetadata(0.0d));

        /// <summary>
        /// DependencyProperty for <see cref="Right"/> property.
        /// </summary>
        public static readonly DependencyProperty RightProperty =
            DependencyProperty.Register(
                "Right",
                typeof(double),
                typeof(SectionElementContainer),
                new PropertyMetadata(0.0d));

        /// <summary>
        /// DependencyProperty for <see cref="Bottom"/> property.
        /// </summary>
        public static readonly DependencyProperty BottomProperty =
            DependencyProperty.Register(
                "Bottom",
                typeof(double),
                typeof(SectionElementContainer),
                new PropertyMetadata(0.0d));

        /// <summary>
        /// DependencyProperty for <see cref="BoundingBox"/> property.
        /// </summary>
        public static readonly DependencyProperty BoundingBoxProperty =
            DependencyProperty.Register(
                "BoundingBox",
                typeof(Rect),
                typeof(SectionElementContainer),
                new PropertyMetadata(Rect.Empty));

        #endregion Dependency Properties

        #region Properties

        /// <summary>
        /// Gets or sets a location of the left bound for the current <see cref="SectionElementContainer"/>
        /// in <see cref="SectionEditorControl.ItemsHost"/> coordinates.
        /// </summary>
        public double Left
        {
            get { return (double)GetValue(BoundingBoxProperty); }
            set { SetValue(BoundingBoxProperty, value); }
        }

        /// <summary>
        /// Gets or sets a location of the top bound for the current <see cref="SectionElementContainer"/>
        /// in <see cref="SectionEditorControl.ItemsHost"/> coordinates.
        /// </summary>
        public double Top
        {
            get { return (double)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        /// <summary>
        /// Gets or sets a location of the right bound for the current <see cref="SectionElementContainer"/>
        /// in <see cref="SectionEditorControl.ItemsHost"/> coordinates.
        /// </summary>
        public double Right
        {
            get { return (double)GetValue(RightProperty); }
            set { SetValue(RightProperty, value); }
        }

        /// <summary>
        /// Gets or sets a location of the bottom bound for the current <see cref="SectionElementContainer"/>
        /// in <see cref="SectionEditorControl.ItemsHost"/> coordinates.
        /// </summary>
        public double Bottom
        {
            get { return (double)GetValue(BottomProperty); }
            set { SetValue(BottomProperty, value); }
        }

        /// <summary>
        /// Gets or sets a bounding box for the current <see cref="SectionElementContainer"/>.
        /// </summary>
        public Rect BoundingBox
        {
            get { return (Rect)GetValue(BoundingBoxProperty); }
            set { SetValue(BoundingBoxProperty, value); }
        }

        #endregion Properties
    }
}
