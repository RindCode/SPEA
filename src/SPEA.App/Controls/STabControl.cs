// ==================================================================================================
// <copyright file="STabControl.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Controls
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Possible horizontal aligmnets for <see cref="STabControl"/> control.
    /// </summary>
    public enum TabPanelHorizontalAlignment
    {
        Left = 0,
        Center = 1,
        Right = 2,
    }

    /// <summary>
    /// Possible vertical aligmnets for <see cref="STabControl"/> control.
    /// </summary>
    ////[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1602:Enumeration items should be documented", Justification = "Self-explanatory.")]
    ////public enum TabPanelVerticalAlignment
    ////{
    ////    Top = 0,
    ////    Center = 1,
    ////    Bottom = 2,
    ////}

    /// <summary>
    /// Represent <see cref="TabControl"/> control with additional features.
    /// </summary>
    public class STabControl : TabControl
    {
        #region Dependency Properties

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="TabsHorizontalAlignment"/> property.
        /// </summary>
        public static readonly DependencyProperty TabsHorizontalAlignmentProperty =
            DependencyProperty.Register(
                nameof(TabsHorizontalAlignment),
                typeof(HorizontalAlignment),
                typeof(STabControl),
                new PropertyMetadata(HorizontalAlignment.Left));

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="TabsVerticalAlignment"/> property.
        /// </summary>
        ////public static readonly DependencyProperty TabsVerticalAlignmentProperty =
        ////    DependencyProperty.Register(
        ////        nameof(TabsVerticalAlignment),
        ////        typeof(VerticalAlignment),
        ////        typeof(ExtendedTabControl),
        ////        new PropertyMetadata(VerticalAlignment.Top));

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="LeadingContent"/> property.
        /// </summary>
        public static readonly DependencyProperty LeadingContentProperty =
            DependencyProperty.Register(
                nameof(LeadingContent),
                typeof(object),
                typeof(STabControl),
                new PropertyMetadata(null));

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="TrailingContent"/> property.
        /// </summary>
        public static readonly DependencyProperty TrailingContentProperty =
            DependencyProperty.Register(
                nameof(TrailingContent),
                typeof(object),
                typeof(STabControl),
                new PropertyMetadata(null));

        #endregion Dependency Properties

        #region Properties

        /// <summary>
        /// Gets or sets a horizontal alignment for contol's tabs.
        /// </summary>
        public HorizontalAlignment TabsHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(TabsHorizontalAlignmentProperty); }
            set { SetValue(TabsHorizontalAlignmentProperty, value); }
        }

        /// <summary>
        /// Gets or sets a vertical alignment for contol's tabs.
        /// </summary>
        ////public VerticalAlignment TabsVerticalAlignment
        ////{
        ////    get { return (VerticalAlignment)GetValue(TabsVerticalAlignmentProperty); }
        ////    set { SetValue(TabsVerticalAlignmentProperty, value); }
        ////}

        /// <summary>
        /// Gets or sets leading content which will be placed to the left or above the tabs
        /// depending on control's orientation.
        /// </summary>
        public object LeadingContent
        {
            get { return (object)GetValue(LeadingContentProperty); }
            set { SetValue(LeadingContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets trailing content which will be placed to the right or below the tabs
        /// depending on control's orientation.
        /// </summary>
        public object TrailingContent
        {
            get { return (object)GetValue(TrailingContentProperty); }
            set { SetValue(TrailingContentProperty, value); }
        }

        #endregion Properties
    }
}
