// ==================================================================================================
// <copyright file="MainViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels.Windows
{
    using System;
    using System.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using SPEA.App.Commands;
    using SPEA.App.ViewModels;
    using SPEA.App.ViewModels.SDocument;

    /// <summary>
    /// The main View Model used by application main window.
    /// </summary>
    public class MainViewModel : WindowBaseViewModel
    {
        #region Fields

        private readonly SDocumentsManagerViewModel _sDocumentsManager;
        private readonly MainMenuViewModel _mainMenuViewModel;
        private readonly ProjectTreeViewModel _projectTreeViewModel;
        private readonly AddGeometryViewModel _addGeometryViewModel;
        private readonly string _requestApplicationCloseCmd = "RequestApplicationClose";

        #endregion Fields

        #region Commands

        #endregion Commands

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="commandsManager">A reference to <see cref="CommandsManager"/> instance.</param>
        /// <param name="sDocumentsManager">A reference to <see cref="SDocumentsManagerViewModel"/> instance.</param>
        /// <param name="mainMenuViewModel">A reference to <see cref="MainMenuViewModel"/> instance.</param>
        /// <param name="projectTreeViewModel">A reference to <see cref="ProjectTreeViewModel"/> instance.</param>
        /// <param name="addGeometryViewModel">A reference to <see cref="AddGeometryViewModel"/> instance.</param>
        public MainViewModel(
            CommandsManager commandsManager,
            SDocumentsManagerViewModel sDocumentsManager,
            MainMenuViewModel mainMenuViewModel,
            ProjectTreeViewModel projectTreeViewModel,
            AddGeometryViewModel addGeometryViewModel)
            : base(commandsManager)
        {
            Current = this;

            _sDocumentsManager = sDocumentsManager ?? throw new ArgumentNullException(nameof(sDocumentsManager));
            _mainMenuViewModel = mainMenuViewModel ?? throw new ArgumentNullException(nameof(mainMenuViewModel));
            _projectTreeViewModel = projectTreeViewModel ?? throw new ArgumentNullException(nameof(projectTreeViewModel));
            _addGeometryViewModel = addGeometryViewModel ?? throw new ArgumentNullException(nameof(addGeometryViewModel));

            CommandsManager.RegisterCommand(_requestApplicationCloseCmd, new RelayCommand<CancelEventArgs>(ExecuteRequestApplicationClose));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets current View Model.
        /// </summary>
        public static MainViewModel? Current { get; private set; }

        /// <summary>
        /// Gets SDocuments controller.
        /// </summary>
        public SDocumentsManagerViewModel SDocumentsManagerViewModelInstance => _sDocumentsManager;

        /// <summary>
        /// Gets main menu view model.
        /// </summary>
        public MainMenuViewModel MainMenuViewModelInstance => _mainMenuViewModel;

        /// <summary>
        /// Gets project tree view model.
        /// </summary>
        public ProjectTreeViewModel ProjectTreeViewModelInstance => _projectTreeViewModel;

        /// <summary>
        /// Gets project tree view model.
        /// </summary>
        public AddGeometryViewModel AddGeometryViewModelInstance => _addGeometryViewModel;

        #endregion Properties

        #region Commands Logic

        /// <summary>
        /// Implements <see cref="RequestApplicationClose"/> command logic.
        /// </summary>
        private void ExecuteRequestApplicationClose(CancelEventArgs? e)
        {
            // Blank.
        }

        #endregion Commands Logic
    }
}
