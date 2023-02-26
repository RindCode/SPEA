// ==================================================================================================
// <copyright file="SElementContainer.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Controls.SViewport
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Represents a UI container (basically - a bounding box) for an element
    /// located inside of <see cref="SViewportItemsHostControl"/>.
    /// </summary>
    [TemplatePart(Name = ContentPresenterName, Type = typeof(ContentPresenter))]
    public class SElementContainer : ContentControl
    {
        #region Fields

        private const string ContentPresenterName = "PART_ContentPresenter";
        private SViewportControl? _itemsOwner;

        #endregion Fields

        #region Dependency Properties

        /// <summary>
        /// DependencyProperty for <see cref="Left"/> property.
        /// </summary>
        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register(
                "Left",
                typeof(double),
                typeof(SElementContainer),
                new FrameworkPropertyMetadata(0.0d));

        /// <summary>
        /// Gets or sets a location of the left bound for the current <see cref="SElementContainer"/>
        /// in <see cref="SViewportControl.ItemsHost"/> coordinates.
        /// </summary>
        public double Left
        {
            get { return (double)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        /// <summary>
        /// DependencyProperty for <see cref="Top"/> property.
        /// </summary>
        public static readonly DependencyProperty TopProperty =
            DependencyProperty.Register(
                "Top",
                typeof(double),
                typeof(SElementContainer),
                new FrameworkPropertyMetadata(0.0d));

        /// <summary>
        /// Gets or sets a location of the top bound for the current <see cref="SElementContainer"/>
        /// in <see cref="SViewportControl.ItemsHost"/> coordinates.
        /// </summary>
        public double Top
        {
            get { return (double)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        /////// <summary>
        /////// DependencyProperty for <see cref="Right"/> property.
        /////// </summary>
        ////public static readonly DependencyProperty RightProperty =
        ////    DependencyProperty.Register(
        ////        "Right",
        ////        typeof(double),
        ////        typeof(SElementContainer),
        ////        new PropertyMetadata(0.0d));

        /////// <summary>
        /////// Gets or sets a location of the right bound for the current <see cref="SElementContainer"/>
        /////// in <see cref="SViewportControl.ItemsHost"/> coordinates.
        /////// </summary>
        ////public double Right
        ////{
        ////    get { return (double)GetValue(RightProperty); }
        ////    set { SetValue(RightProperty, value); }
        ////}

        /////// <summary>
        /////// DependencyProperty for <see cref="Bottom"/> property.
        /////// </summary>
        ////public static readonly DependencyProperty BottomProperty =
        ////    DependencyProperty.Register(
        ////        "Bottom",
        ////        typeof(double),
        ////        typeof(SElementContainer),
        ////        new PropertyMetadata(0.0d));

        /////// <summary>
        /////// Gets or sets a location of the bottom bound for the current <see cref="SElementContainer"/>
        /////// in <see cref="SViewportControl.ItemsHost"/> coordinates.
        /////// </summary>
        ////public double Bottom
        ////{
        ////    get { return (double)GetValue(BottomProperty); }
        ////    set { SetValue(BottomProperty, value); }
        ////}

        /// <summary>
        /// DependencyProperty for <see cref="BoundingBox"/> property.
        /// </summary>
        public static readonly DependencyProperty BoundingBoxProperty =
            DependencyProperty.Register(
                "BoundingBox",
                typeof(Rect),
                typeof(SElementContainer),
                new FrameworkPropertyMetadata(Rect.Empty));

        /// <summary>
        /// Gets or sets a bounding box for the current <see cref="SElementContainer"/>.
        /// </summary>
        public Rect BoundingBox
        {
            get { return (Rect)GetValue(BoundingBoxProperty); }
            set { SetValue(BoundingBoxProperty, value); }
        }

        /// <summary>
        /// DependencyProperty for <see cref="ApplyTransform"/> property.
        /// </summary>
        public static readonly DependencyProperty ApplyTransformProperty =
            DependencyProperty.Register(
                "ApplyTransform",
                typeof(Transform),
                typeof(SElementContainer),
                new FrameworkPropertyMetadata(
                    default(Transform),
                    new PropertyChangedCallback(OnApplyTransformChanged)));

        /// <summary>
        /// Gets or sets transforms for the current <see cref="SElementContainer"/>.
        /// </summary>
        public Rect ApplyTransform
        {
            get { return (Rect)GetValue(ApplyTransformProperty); }
            set { SetValue(ApplyTransformProperty, value); }
        }

        #endregion Dependency Properties

        #region Properties

        /////// <summary>
        /////// Gets a value indicating whether this container is valid.
        /////// </summary>
        ////public bool IsValid
        ////{
        ////    get
        ////    {
        ////        return (Height != 0 || ActualHeight != 0) && (Width != 0 || ActualWidth != 0)
        ////            && (!double.IsNaN(Height) && !double.IsNaN(ActualHeight)) && (!double.IsNaN(Width) && !double.IsNaN(ActualWidth));
        ////    }
        ////}

        /// <summary>
        /// Gets the <see cref="SElementContainer"/> translate transform.
        /// </summary>
        public TranslateTransform? TranslateTransform => RenderTransform is TransformGroup group ? group.Children.OfType<TranslateTransform>().FirstOrDefault() : null;

        /// <summary>
        /// Gets the <see cref="SElementContainer"/> scale transform.
        /// </summary>
        public ScaleTransform? ScaleTransform => RenderTransform is TransformGroup group ? group.Children.OfType<ScaleTransform>().FirstOrDefault() : null;

        /// <summary>
        /// Gets the <see cref="SViewportControl"/> that owns <see cref="SElementContainer"/> items container type.
        /// </summary>
        public SViewportControl? ItemsOwner => _itemsOwner ??= ItemsControl.ItemsControlFromItemContainer(this) as SViewportControl;

        #endregion Properties

        #region Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // TODO: REMOVE CODE
            var trasnformGroup = new TransformGroup();
            trasnformGroup.Children.Add(new TranslateTransform(100, 200));
            trasnformGroup.Children.Add(new ScaleTransform());
            RenderTransform = trasnformGroup;
        }

        // Is called when the element transform has changed.
        private static void OnApplyTransformChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (SElementContainer)d;
            var newValue = (Transform)e.NewValue;

            if (newValue != source.RenderTransform)
            {
                source.UpdateTransform(newValue);
            }
        }

        // Updates the current transform.
        private void UpdateTransform(Transform transform)
        {
            // Call Arrange() on ItemsHost to re-calculate bounding box.
            RenderTransform = transform.Clone();
            _itemsOwner?.ItemsHost?.InvalidateArrange();
        }

        #endregion Methods
    }
}
