// ==================================================================================================
// <copyright file="ProjectTreeView.xaml.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Views.UserControls
{
    using System.Collections;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for ProjectTreeView.xaml.
    /// </summary>
    public partial class ProjectTreeView : UserControl
    {
        #region Dependency Properties

        /// <summary>
        /// DependencyProperty for <see cref="ItemsSource"/> property.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(IEnumerable),
                typeof(ProjectTreeView),
                new PropertyMetadata(default));

        /// <summary>
        /// Gets or sets items source.
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        #endregion Dependency Properties

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectTreeView"/> class.
        /// </summary>
        public ProjectTreeView()
        {
            InitializeComponent();
        }
    }
}
