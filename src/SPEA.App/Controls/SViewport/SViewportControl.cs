// ==================================================================================================
// <copyright file="SViewportControl.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Controls.SViewport
{
    using System;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    /// <summary>
    /// Defines a flexible area with infinite panning and zooming capabilities.
    /// </summary>
    [TemplatePart(Name = InfinitePanelPartName, Type = typeof(InfinitePanel))]
    [TemplatePart(Name = ItemsHostPartName, Type = typeof(SViewportItemsHostControl))]
    [TemplatePart(Name = MajorGridPartName, Type = typeof(Border))]
    [TemplatePart(Name = MinorGridPartName, Type = typeof(Border))]
    public class SViewportControl : MultiSelector
    {
        #region Fields

        // Must be defined in ControlTemplate.
        private const string InfinitePanelPartName = "PART_InfinitePanel";
        private const string ItemsHostPartName = "PART_ItemsHost";
        private const string MinorGridPartName = "PART_MinorGrid";
        private const string MajorGridPartName = "PART_MajorGrid";

        ////private readonly List<int> _containersIndexes = new List<int>();
        private InfinitePanel? _infinitePanel = null;
        private SViewportItemsHostControl? _itemsHost = null;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SViewportControl"/> class.
        /// </summary>
        public SViewportControl()
        {
            // Blank.
        }

        #endregion Constructors

        #region Dependency Properties

        /// <summary>
        /// DependencyProperty for <see cref="CanSelectMultipleItems"/> property.
        /// </summary>
        public static readonly DependencyProperty CanSelectMultipleItemsProperty =
            DependencyProperty.Register(
                "CanSelectMultipleItems",
                typeof(bool),
                typeof(SViewportControl),
                new FrameworkPropertyMetadata(
                    true,
                    OnCanSelectMultipleItemsChanged));

        /// <summary>
        /// Gets or sets a value indicating whether the current <see cref="SViewportControl"/>
        /// allows to select multiple items.
        /// </summary>
        public bool CanSelectMultipleItems
        {
            get { return (bool)GetValue(CanSelectMultipleItemsProperty); }
            set { SetValue(CanSelectMultipleItemsProperty, value); }
        }

        /// <summary>
        /// DependencyProperty for <see cref="ScrollFactor"/> property.
        /// </summary>
        public static readonly DependencyProperty ScrollFactorProperty =
            DependencyProperty.Register(
                "ScrollFactor",
                typeof(double),
                typeof(SViewportControl),
                new FrameworkPropertyMetadata(
                    0.05d));

        /// <summary>
        /// Gets or sets the scroll factor value.
        /// </summary>
        public double ScrollFactor
        {
            get { return (double)GetValue(ScrollFactorProperty); }
            set { SetValue(ScrollFactorProperty, value); }
        }

        /// <summary>
        /// DependencyProperty for <see cref="ContentScale"/> property.
        /// </summary>
        public static readonly DependencyProperty ContentScaleProperty =
            DependencyProperty.Register(
                "ContentScale",
                typeof(double),
                typeof(SViewportControl),
                new FrameworkPropertyMetadata(
                    1.0d));

        /// <summary>
        /// Gets or sets the scale value.
        /// </summary>
        public double ContentScale
        {
            get { return (double)GetValue(ContentScaleProperty); }
            set { SetValue(ContentScaleProperty, value); }
        }

        /// <summary>
        /// DependencyProperty for <see cref="MinorGridEnabled"/> property.
        /// </summary>
        public static readonly DependencyProperty MinorGridEnabledProperty =
            DependencyProperty.Register(
                "MinorGridEnabled",
                typeof(bool),
                typeof(SViewportControl),
                new FrameworkPropertyMetadata(true));

        /// <summary>
        /// Gets or sets a value indicating whether the minor grid is enabled or not.
        /// </summary>
        public bool MinorGridEnabled
        {
            get { return (bool)GetValue(MinorGridEnabledProperty); }
            set { SetValue(MinorGridEnabledProperty, value); }
        }

        /// <summary>
        /// DependencyProperty for <see cref="MajorGridEnabled"/> property.
        /// </summary>
        public static readonly DependencyProperty MajorGridEnabledProperty =
            DependencyProperty.Register(
                "MajorGridEnabled",
                typeof(bool),
                typeof(SViewportControl),
                new FrameworkPropertyMetadata(true));

        /// <summary>
        /// Gets or sets a value indicating whether the major grid is enabled or not.
        /// </summary>
        public bool MajorGridEnabled
        {
            get { return (bool)GetValue(MajorGridEnabledProperty); }
            set { SetValue(MajorGridEnabledProperty, value); }
        }

        /////// <summary>
        /////// DependencyProperty for <see cref="MinorGridSpacing"/> property.
        /////// </summary>
        ////public static readonly DependencyProperty MinorGridSpacingProperty =
        ////    DependencyProperty.Register(
        ////        "MinorGridSpacing",
        ////        typeof(double),
        ////        typeof(SViewportControl),
        ////        new PropertyMetadata(10.0d));

        /////// <summary>
        /////// DependencyProperty for <see cref="MajorGridSpacing"/> property.
        /////// </summary>
        ////public static readonly DependencyProperty MajorGridSpacingProperty =
        ////    DependencyProperty.Register(
        ////        "MajorGridSpacing",
        ////        typeof(double),
        ////        typeof(SViewportControl),
        ////        new PropertyMetadata(100.0d));

        /// <summary>
        /// DependencyProperty for <see cref="MinorGridViewport"/> property.
        /// </summary>
        public static readonly DependencyProperty MinorGridViewportProperty =
            DependencyProperty.Register(
                "MinorGridViewport",
                typeof(Rect),
                typeof(SViewportControl),
                new FrameworkPropertyMetadata(new Rect(0, 0, 10, 10)));

        /// <summary>
        /// Gets or sets a value of a minor grid viewport.
        /// </summary>
        public Rect MinorGridViewport
        {
            get { return (Rect)GetValue(MinorGridViewportProperty); }
            set { SetValue(MinorGridViewportProperty, value); }
        }

        /// <summary>
        /// DependencyProperty for <see cref="MajorGridViewport"/> property.
        /// </summary>
        public static readonly DependencyProperty MajorGridViewportProperty =
            DependencyProperty.Register(
                "MajorGridViewport",
                typeof(Rect),
                typeof(SViewportControl),
                new FrameworkPropertyMetadata(new Rect(0, 0, 100, 100)));

        /// <summary>
        /// Gets or sets a value of a major grid viewport.
        /// </summary>
        public Rect MajorGridViewport
        {
            get { return (Rect)GetValue(MajorGridViewportProperty); }
            set { SetValue(MajorGridViewportProperty, value); }
        }

        /////// <summary>
        /////// DependencyProperty for <see cref="PanningKey"/> property.
        /////// </summary>
        ////public static readonly DependencyProperty PanningKeyProperty =
        ////    DependencyProperty.Register(
        ////        "PanningKey",
        ////        typeof(Key),
        ////        typeof(SViewportControl),
        ////        new PropertyMetadata(Key.None));

        #endregion Dependency Properties

        #region Properties

        /// <summary>
        /// Gets the items host control.
        /// </summary>
        public SViewportItemsHostControl? ItemsHost => _itemsHost;

        /////// <summary>
        /////// Gets or sets the value of a minor grid spacing.
        /////// </summary>
        ////public double MinorGridSpacing
        ////{
        ////    get { return (double)GetValue(MinorGridSpacingProperty); }
        ////    set { SetValue(MinorGridSpacingProperty, value); }
        ////}

        /////// <summary>
        /////// Gets or sets the value of a major grid spacing.
        /////// </summary>
        ////public double MajorGridSpacing
        ////{
        ////    get { return (double)GetValue(MajorGridSpacingProperty); }
        ////    set { SetValue(MajorGridSpacingProperty, value); }
        ////}

        /////// <summary>
        /////// Gets or sets <see cref="Key"/> that activates panning mode.
        /////// </summary>
        ////public Key PanningKey
        ////{
        ////    get { return (Key)GetValue(PanningKeyProperty); }
        ////    set { SetValue(PanningKeyProperty, value); }
        ////}

        #endregion Properties

        #region Methods

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            _itemsHost = (SViewportItemsHostControl)GetTemplateChild(ItemsHostPartName);
            _itemsHost.ItemsOwner = this;

            _infinitePanel = (InfinitePanel)GetTemplateChild(InfinitePanelPartName);
            _infinitePanel.Initialize(this);
        }

        // Container types and overrides: http://drwpf.com/blog/2008/07/20/itemscontrol-g-is-for-generator/

        /// <summary>
        /// Specifies a <see cref="SViewportControl"/> item container type.
        /// </summary>
        /// <remarks>
        /// Since <see cref="SViewportControl"/> inherits from <see cref="ItemsControl"/>,
        /// its <see cref="ItemContainerGenerator"/> will use this method to get the appropriate
        /// container type.
        /// </remarks>
        /// <returns><see cref="SElementContainer"/> item container.</returns>
        protected override DependencyObject GetContainerForItemOverride() => new SElementContainer();

        /// <summary>
        /// Is used by <see cref="ItemContainerGenerator"/> to check whether a given item is 
        /// already a type of <see cref="SElementContainer"/> before asking <see cref="ItemsControl"/>
        /// to create a new container using <see cref="GetContainerForItemOverride"/>.
        /// </summary>
        /// <param name="item">An item to be checked.</param>
        /// <returns><see langword="true"/> if a given item is <see cref="SElementContainer"/>, otherwise <see langword="false"/>.</returns>
        protected override bool IsItemItsOwnContainerOverride(object item) => item is SElementContainer;

        /// <summary>
        /// Invoked when the value of the <see cref="ItemsControl.Items"/> property changes.
        /// </summary>
        /// <param name="e">Event data.</param>
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            ////Debug.WriteLine($"oldindex={e.OldStartingIndex}");
            ////Debug.WriteLine($"newindex={e.NewStartingIndex}");
            ////Debug.WriteLine($"action={e.Action}");

            // NewStartingIndex = 0 when the first item is added.
            ////if (e.NewStartingIndex != -1 && e.Action == NotifyCollectionChangedAction.Add)
            ////{
            ////    var container = (SElementContainer)ItemContainerGenerator.ContainerFromItem(e.NewStartingIndex);
            ////    if (container.IsValid)
            ////    {
            ////        _containersIndexes.Add(e.NewStartingIndex);
            ////    }
            ////}
        }

        // CanSelectMultipleItemsProperty DP callback.
        private static void OnCanSelectMultipleItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SViewportControl)d).UpdateCanSelectMultipleItems((bool)e.NewValue);
        }

        // Updates CanSelectMultipleItems property in MultiSelector class, since it's a simple property.
        // This callback keeps a connection between a custom DP and the ordinary one.
        private void UpdateCanSelectMultipleItems(bool value)
        {
            base.CanSelectMultipleItems = value;
        }

        #endregion Methods
    }
}
