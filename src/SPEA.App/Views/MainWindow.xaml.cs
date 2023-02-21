// ==================================================================================================
// <copyright file="MainWindow.xaml.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.Views
{
    using System.Windows.Automation.Peers;
    using SPEA.App.Controls;
    using SPEA.App.Utils.Helpers;

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

        /// <inheritdoc/>
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            // TODO: Fixes Visual Studio memory leak.
            //       For some reason there were AutomationPeer objects left in the memory for SViewportControl
            //       even after all realted view models were removed from the AvalonDock DockingManager collection.
            //       We override it here to prevent it walking down the tree and and immediately return an empty list.
            // https://www.syncfusion.com/kb/3860/how-to-release-the-memory-held-by-automationpeer-in-wpf-components
            // https://stackoverflow.com/questions/17297539/can-ui-automation-be-disabled-for-an-entire-wpf-4-0-app

            return new CustomWindowAutomationPeer(this);
        }
    }


}
