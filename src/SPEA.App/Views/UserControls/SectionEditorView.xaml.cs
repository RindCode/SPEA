// ==================================================================================================
// <copyright file="SectionEditorView.xaml.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Views.UserControls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using SPEA.App.Controls.SViewport;

    /// <summary>
    /// Interaction logic for SectionEditorView.xaml.
    /// </summary>
    public partial class SectionEditorView : UserControl
    {
        #region Dependency Properties

        /// <summary>
        /// DependencyProperty for <see cref="PanningKey"/> property.
        /// </summary>
        public static readonly DependencyProperty PanningKeyProperty =
            DependencyProperty.Register(
                "PanningKey",
                typeof(Key),
                typeof(SViewportControl),
                new PropertyMetadata(Key.None));

        #endregion Dependency Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionEditorView"/> class.
        /// </summary>
        public SectionEditorView()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets <see cref="Key"/> that activates panning mode.
        /// </summary>
        public Key PanningKey
        {
            get { return (Key)GetValue(PanningKeyProperty); }
            set { SetValue(PanningKeyProperty, value); }
        }

        #endregion Properties
    }
}
