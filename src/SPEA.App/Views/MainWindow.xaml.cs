// ==================================================================================================
// <copyright file="MainWindow.xaml.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Views
{
    using SPEA.App.Controls;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : WindowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
            : base()
        {
            InitializeComponent();

            Current = this;
        }

        /// <summary>
        /// Gets current <see cref="MainWindow"/> instance.
        /// </summary>
        public static MainWindow Current { get; private set; }
    }
}
