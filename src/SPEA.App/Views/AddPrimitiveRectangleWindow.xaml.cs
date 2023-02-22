// ==================================================================================================
// <copyright file="AddPrimitiveRectangleWindow.xaml.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Views
{
    using SPEA.App.Controls;

    /// <summary>
    /// Interaction logic for AddPrimitiveRectangleWindow.xaml.
    /// </summary>
    public partial class AddPrimitiveRectangleWindow : WindowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddPrimitiveRectangleWindow"/> class.
        /// </summary>
        public AddPrimitiveRectangleWindow()
        {
            Owner = SPEA_Application.Current.MainWindow;

            InitializeComponent();
        }
    }
}
