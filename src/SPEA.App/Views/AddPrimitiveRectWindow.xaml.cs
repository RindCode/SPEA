// ==================================================================================================
// <copyright file="AddPrimitiveRectWindow.xaml.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Views
{
    using SPEA.App.Controls;

    /// <summary>
    /// Interaction logic for AddPrimitiveRectWindow.xaml.
    /// </summary>
    public partial class AddPrimitiveRectWindow : WindowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddPrimitiveRectWindow"/> class.
        /// </summary>
        public AddPrimitiveRectWindow()
        {
            Owner = SPEA_Application.Current.MainWindow;

            InitializeComponent();
        }
    }
}
