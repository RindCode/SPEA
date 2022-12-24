// ==================================================================================================
// <copyright file="NewSectionWindow.xaml.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Views
{
    using Microsoft.Extensions.DependencyInjection;
    using SPEA.App.Controls;
    using SPEA.App.ViewModels;

    /// <summary>
    /// Interaction logic for NewSectionDialog.xaml.
    /// </summary>
    public partial class NewSectionWindow : WindowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewSectionWindow"/> class.
        /// </summary>
        public NewSectionWindow()
        {
            Owner = SPEA_Application.Current.MainWindow;

            InitializeComponent();
        }
    }
}
