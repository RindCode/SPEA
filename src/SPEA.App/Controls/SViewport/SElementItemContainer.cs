// ==================================================================================================
// <copyright file="SElementItemContainer.cs" company="Dmitry Poberezhnyy">
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
    using SPEA.App.Utils.Helpers;

    /// <summary>
    /// Represents a UI container (basically - a bounding box) for an element
    /// located inside of <see cref="SViewportItemsHostControl"/>.
    /// </summary>
    [TemplatePart(Name = ContentPresenterName, Type = typeof(ContentPresenter))]
    public class SElementItemContainer : ContentControl
    {
        #region Fields

        private const string ContentPresenterName = "PART_ContentPresenter";
        private SViewportControl? _itemsOwner;

        #endregion Fields

        #region Dependency Properties

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="GetApplyTransform(DependencyObject)"/> getter
        /// and <see cref="SetApplyTransform(DependencyObject, Transform)"/> setter.
        /// </summary>
        public static readonly DependencyProperty ApplyTransformProperty =
            DependencyProperty.RegisterAttached(
                "ApplyTransform",
                typeof(Transform),
                typeof(SElementItemContainer),
                new FrameworkPropertyMetadata(
                    default(Transform),
                    new PropertyChangedCallback(OnApplyTransformChanged)));

        /// <summary>
        /// Gets the value of <see cref="ApplyTransformProperty"/>.
        /// </summary>
        /// <param name="obj">An object the value is get from.</param>
        /// <returns>Transform value.</returns>
        public static Transform GetApplyTransform(DependencyObject obj)
        {
            return (Transform)obj.GetValue(ApplyTransformProperty);
        }

        /// <summary>
        /// Sets the value of <see cref="ApplyTransformProperty"/>.
        /// </summary>
        /// <param name="obj">An object the value is set to.</param>
        /// <param name="value">Transform value.</param>
        public static void SetApplyTransform(DependencyObject obj, Transform value)
        {
            obj.SetValue(ApplyTransformProperty, value);
        }

        /// <summary>
        /// DependencyProperty for <see cref="Left"/> property.
        /// </summary>
        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register(
                nameof(Left),
                typeof(double),
                typeof(SElementItemContainer),
                new FrameworkPropertyMetadata(0.0d));

        /// <summary>
        /// Gets or sets a location of the left bound for the current <see cref="SElementItemContainer"/>
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
                nameof(Top),
                typeof(double),
                typeof(SElementItemContainer),
                new FrameworkPropertyMetadata(0.0d));

        /// <summary>
        /// Gets or sets a location of the top bound for the current <see cref="SElementItemContainer"/>
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
        ////        typeof(SElementItemContainer),
        ////        new PropertyMetadata(0.0d));

        /////// <summary>
        /////// Gets or sets a location of the right bound for the current <see cref="SElementItemContainer"/>
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
        ////        typeof(SElementItemContainer),
        ////        new PropertyMetadata(0.0d));

        /////// <summary>
        /////// Gets or sets a location of the bottom bound for the current <see cref="SElementItemContainer"/>
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
                nameof(BoundingBox),
                typeof(Rect),
                typeof(SElementItemContainer),
                new FrameworkPropertyMetadata(Rect.Empty));

        /// <summary>
        /// Gets or sets a bounding box for the current <see cref="SElementItemContainer"/>.
        /// </summary>
        public Rect BoundingBox
        {
            get { return (Rect)GetValue(BoundingBoxProperty); }
            set { SetValue(BoundingBoxProperty, value); }
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
        ////        return (H != 0 || ActualHeight != 0) && (W != 0 || ActualWidth != 0)
        ////            && (!double.IsNaN(H) && !double.IsNaN(ActualHeight)) && (!double.IsNaN(W) && !double.IsNaN(ActualWidth));
        ////    }
        ////}

        /// <summary>
        /// Gets the <see cref="SElementItemContainer"/> translate transform.
        /// </summary>
        public TranslateTransform? TranslateTransform => RenderTransform is TransformGroup group ? group.Children.OfType<TranslateTransform>().FirstOrDefault() : null;

        /// <summary>
        /// Gets the <see cref="SElementItemContainer"/> scale transform.
        /// </summary>
        public ScaleTransform? ScaleTransform => RenderTransform is TransformGroup group ? group.Children.OfType<ScaleTransform>().FirstOrDefault() : null;

        /// <summary>
        /// Gets the <see cref="SViewportControl"/> that owns <see cref="SElementItemContainer"/> items container type.
        /// </summary>
        public SViewportControl? ItemsOwner => _itemsOwner ??= ItemsControl.ItemsControlFromItemContainer(this) as SViewportControl;

        #endregion Properties

        #region Methods

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        // Is called when the element transform has changed.
        private static void OnApplyTransformChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (UIElement)d;
            var parent = VisualTreeHelperEx.FindParent<SElementItemContainer>(d);

            var newValue = (Transform)e.NewValue;
            if (newValue != source.RenderTransform)
            {
                parent?.UpdateTransform(newValue);
            }
        }

        // Updates the current transform.
        private void UpdateTransform(Transform transform)
        {
            // Call Arrange() on ItemsHost to re-calculate the bounding box.
            RenderTransform = transform.Clone();
            _itemsOwner?.ItemsHost?.InvalidateArrange();
        }

        #endregion Methods
    }
}
