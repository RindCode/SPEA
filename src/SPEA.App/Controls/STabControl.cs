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
        /// <summary>
        /// Left-aligned tab panel.
        /// </summary>
        Left = 0,

        /// <summary>
        /// Right-aligned tab panel.
        /// </summary>
        Right = 1,
    }

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
        /// Gets or sets a horizontal alignment for contol's tabs.
        /// </summary>
        public HorizontalAlignment TabsHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(TabsHorizontalAlignmentProperty); }
            set { SetValue(TabsHorizontalAlignmentProperty, value); }
        }

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
        /// Gets or sets leading content which will be placed to the left or above the tabs
        /// depending on control's orientation.
        /// </summary>
        public object LeadingContent
        {
            get { return (object)GetValue(LeadingContentProperty); }
            set { SetValue(LeadingContentProperty, value); }
        }

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="TrailingContent"/> property.
        /// </summary>
        public static readonly DependencyProperty TrailingContentProperty =
            DependencyProperty.Register(
                nameof(TrailingContent),
                typeof(object),
                typeof(STabControl),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets trailing content which will be placed to the right or below the tabs
        /// depending on control's orientation.
        /// </summary>
        public object TrailingContent
        {
            get { return (object)GetValue(TrailingContentProperty); }
            set { SetValue(TrailingContentProperty, value); }
        }

        #endregion Dependency Properties
    }
}
