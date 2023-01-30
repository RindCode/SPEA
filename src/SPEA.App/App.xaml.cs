// ==================================================================================================
// <copyright file="App.xaml.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App
{
    using System;
    using System.Windows;
    using System.Windows.Threading;
    using Microsoft.Extensions.DependencyInjection;
    using SPEA.App.Extensions.Markup;
    using SPEA.App.ViewModels;
    using SPEA.App.Views;

    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class SPEA_Application : Application
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SPEA_Application"/> class.
        /// </summary>
        public SPEA_Application()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets current application instance.
        /// </summary>
        public static new SPEA_Application Current => (SPEA_Application)Application.Current;

        /// <summary>
        /// Gets <see cref="IServiceProvider"/> for this application, containing various app services.
        /// </summary>
        public IServiceProvider Services { get; private set; }

        /// <summary>
        /// Gets main window instance of this application.
        /// </summary>
        public MainWindow MainWindowInstance { get; private set; }

        #endregion Properties

        #region Methods

        // Performs startup actions.
        private void App_Startup(object sender, StartupEventArgs e)
        {
            Services = Utils.Services.ServiceProvider.ConfigureServices();

            DISourceExtension.Resolver = (type) => { return Services.GetRequiredService(type); };

            var mainViewModel = Services.GetRequiredService<MainViewModel>();

            MainWindowInstance = new ()
            {
                DataContext = mainViewModel,
            };

            MainWindowInstance.Show();
        }

        // Handles unhandled exceptions (very descriptive).
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // TODO: rework with a custom implementation of message box dialog.

            ////var fullMessage = $"Source: {e.Exception.Source}\n\n{e.Exception.Message}";
            ////var exTitle = (string)Current.Resources["S.MessageBox.SDocument.UnhandledEx"];

            ////MessageBoxService.Show(fullMessage, exTitle, MessageBoxButton.OK, MessageBoxImage.Error);

            ////e.Handled = true;
        }

        #endregion Methods
    }
}
