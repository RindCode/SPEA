// ==================================================================================================
// <copyright file="SectionEditorControl.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Controls.SectionEditor
{
    using SPEA.Geometry.Primitives;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    /// <summary>
    /// Defines a flexible area with infinite panning and zooming capabilities to
    /// arrange and edit built-up cross-section shapes.
    /// </summary>
    [TemplatePart(Name = InfinitePanelPartName, Type = typeof(InfinitePanel))]
    [TemplatePart(Name = MajorGridPartName, Type = typeof(Border))]
    [TemplatePart(Name = MinorGridPartName, Type = typeof(Border))]
    [TemplatePart(Name = ItemsHostPartName, Type = typeof(SectionEditorItemsHostControl))]
    public class SectionEditorControl : MultiSelector
    {
        #region Fields

        // Must be defined in ControlTemplate.
        private const string InfinitePanelPartName = "PART_InfinitePanel";
        private const string MinorGridPartName = "PART_MinorGrid";
        private const string MajorGridPartName = "PART_MajorGrid";
        private const string ItemsHostPartName = "PART_ItemsHost";

        // Holds the reference to panning and zoom panel layer (child).
        private InfinitePanel _infinitePanelContainer = null;

        // Holds the reference to panning and zoom panel layer (child).
        private SectionEditorItemsHostControl _itemsHost = null;

        #endregion Fields

        #region Dependency Properties

        /// <summary>
        /// DependencyProperty for <see cref="MinorGridEnabled"/> property.
        /// </summary>
        public static readonly DependencyProperty MinorGridEnabledProperty =
            DependencyProperty.Register(
                "MinorGridEnabled",
                typeof(bool),
                typeof(SectionEditorControl),
                new PropertyMetadata(true));

        /// <summary>
        /// DependencyProperty for <see cref="MajorGridEnabled"/> property.
        /// </summary>
        public static readonly DependencyProperty MajorGridEnabledProperty =
            DependencyProperty.Register(
                "MajorGridEnabled",
                typeof(bool),
                typeof(SectionEditorControl),
                new PropertyMetadata(true));

        /////// <summary>
        /////// DependencyProperty for <see cref="MinorGridSpacing"/> property.
        /////// </summary>
        ////public static readonly DependencyProperty MinorGridSpacingProperty =
        ////    DependencyProperty.Register(
        ////        "MinorGridSpacing",
        ////        typeof(double),
        ////        typeof(SectionEditorControl),
        ////        new PropertyMetadata(10.0d));

        /////// <summary>
        /////// DependencyProperty for <see cref="MajorGridSpacing"/> property.
        /////// </summary>
        ////public static readonly DependencyProperty MajorGridSpacingProperty =
        ////    DependencyProperty.Register(
        ////        "MajorGridSpacing",
        ////        typeof(double),
        ////        typeof(SectionEditorControl),
        ////        new PropertyMetadata(100.0d));

        /// <summary>
        /// DependencyProperty for <see cref="MinorGridViewport"/> property.
        /// </summary>
        public static readonly DependencyProperty MinorGridViewportProperty =
            DependencyProperty.Register(
                "MinorGridViewport",
                typeof(Rect),
                typeof(SectionEditorControl),
                new PropertyMetadata(new Rect(0, 0, 10, 10)));

        /// <summary>
        /// DependencyProperty for <see cref="MajorGridViewport"/> property.
        /// </summary>
        public static readonly DependencyProperty MajorGridViewportProperty =
            DependencyProperty.Register(
                "MajorGridViewport",
                typeof(Rect),
                typeof(SectionEditorControl),
                new PropertyMetadata(new Rect(0, 0, 100, 100)));

        /////// <summary>
        /////// DependencyProperty for <see cref="PanningKey"/> property.
        /////// </summary>
        ////public static readonly DependencyProperty PanningKeyProperty =
        ////    DependencyProperty.Register(
        ////        "PanningKey",
        ////        typeof(Key),
        ////        typeof(SectionEditorControl),
        ////        new PropertyMetadata(Key.None));

        #endregion Dependency Properties

        #region Properties

        /// <summary>
        /// Gets the items host control.
        /// </summary>
        internal SectionEditorItemsHostControl ItemsHost => _itemsHost;

        /// <summary>
        /// Gets or sets a value indicating whether the minor grid is enabled or not.
        /// </summary>
        public bool MinorGridEnabled
        {
            get { return (bool)GetValue(MinorGridEnabledProperty); }
            set { SetValue(MinorGridEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the major grid is enabled or not.
        /// </summary>
        public bool MajorGridEnabled
        {
            get { return (bool)GetValue(MajorGridEnabledProperty); }
            set { SetValue(MajorGridEnabledProperty, value); }
        }

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

        /// <summary>
        /// Gets or sets a value of a minor grid viewport.
        /// </summary>
        public Rect MinorGridViewport
        {
            get { return (Rect)GetValue(MinorGridViewportProperty); }
            set { SetValue(MinorGridViewportProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value of a major grid viewport.
        /// </summary>
        public Rect MajorGridViewport
        {
            get { return (Rect)GetValue(MajorGridViewportProperty); }
            set { SetValue(MajorGridViewportProperty, value); }
        }

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
            _infinitePanelContainer = (InfinitePanel)GetTemplateChild(InfinitePanelPartName);
            if (_infinitePanelContainer == null)
            {
                throw new NullReferenceException($"Couldn't find the element with the following PART_ name: {InfinitePanelPartName}");
            }

            _infinitePanelContainer.Initialize(this);

            _itemsHost = (SectionEditorItemsHostControl)GetTemplateChild(ItemsHostPartName);
            if (_itemsHost == null)
            {
                throw new NullReferenceException($"Couldn't find the element with the following PART_ name: {ItemsHostPartName}");
            }

            _itemsHost.ItemsOwner = this;
        }

        #endregion Methods
    }
}
