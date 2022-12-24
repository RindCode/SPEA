// ==================================================================================================
// <copyright file="MainViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using CommunityToolkit.Mvvm.Input;
    using SPEA.App.Controllers;

    /// <summary>
    /// The main View Model used by application main window.
    /// </summary>
    public class MainViewModel : WindowBaseViewModel
    {
        #region Fields

        // Controller for SDocuments.
        private SDocumentsManager _sDocumentsManager;

        // MainMenuViewModelInstance backing field.
        private MainMenuViewModel _mainMenuViewModel;

        #endregion Fields

        #region Commands

        #endregion Commands

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="sDocumentsManager">A reference to <see cref="SDocumentsManager"/> instance.</param>
        /// <param name="mainMenuViewModel">A reference to <see cref="MainMenuViewModel"/> instance.</param>
        public MainViewModel(
            SDocumentsManager sDocumentsManager,
            MainMenuViewModel mainMenuViewModel)
        {
            Current = this;

            _sDocumentsManager = sDocumentsManager ?? throw new ArgumentNullException(nameof(sDocumentsManager));
            _mainMenuViewModel = mainMenuViewModel ?? throw new ArgumentNullException(nameof(mainMenuViewModel));

            RequestApplicationClose = new RelayCommand<CancelEventArgs>(ExecuteRequestApplicationClose);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets current View Model.
        /// </summary>
        public static MainViewModel Current { get; private set; }

        /// <summary>
        /// Gets main menu view model.
        /// </summary>
        public MainMenuViewModel MainMenuViewModelInstance => _mainMenuViewModel;

        /// <summary>
        /// Gets SDocuments controller.
        /// </summary>
        public SDocumentsManager SDocumentsManagerInstance => _sDocumentsManager;

        #endregion Properties

        #region Commands

        /// <summary>
        /// Gets a command which is invoked directly after <see cref="Window.Close"/> is called.
        /// </summary>
        public RelayCommand<CancelEventArgs> RequestApplicationClose { get; private set; }

        #endregion Commands

        #region Commands Logic

        /// <summary>
        /// Implements <see cref="RequestApplicationClose"/> command logic.
        /// </summary>
        private void ExecuteRequestApplicationClose(CancelEventArgs e)
        {
            // e.Cancel = false by default.
            // Check if there are any unsaved changes among the opened documents.
            var isCanceled = _sDocumentsManager.RemoveAllDocumentsWithConfirmation();
            if (isCanceled)
            {
                e.Cancel = true;
            }
        }

        #endregion Commands Logic

        #region Methods

        #endregion Methods
    }
}
