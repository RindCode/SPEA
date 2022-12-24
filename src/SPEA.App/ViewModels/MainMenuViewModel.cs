// ==================================================================================================
// <copyright file="MainMenuViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels
{
    using System;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using SPEA.App.Controllers;
    using SPEA.App.Utils.Services;
    using SPEA.App.ViewModels.Interfaces;

    /// <summary>
    /// A view model used by application main menu.
    /// </summary>
    public class MainMenuViewModel : ObservableObject
    {
        #region Fields

        // Controller for SDocuments.
        private SDocumentsManager _sDocumentsManager;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenuViewModel"/> class.
        /// </summary>
        /// <param name="owner">A reference to a parent view model.</param>
        /// <param name="sDocumentsManager">A reference to <see cref="SDocumentsManager"/> instance.</param>
        public MainMenuViewModel(SDocumentsManager sDocumentsManager)
        {
            _sDocumentsManager = sDocumentsManager ?? throw new ArgumentNullException(nameof(sDocumentsManager));
            _sDocumentsManager.CloseAllDocumentsCommand.CanExecuteChanged += FileCloseAllCommand_CanExecuteChanged;
            _sDocumentsManager.CloseAllButCommand.CanExecuteChanged += FileCloseOthersCommand_CanExecuteChanged;

            FileNewCommand = new RelayCommand(ExecuteFileNewCommand);
            FileCloseCommand = new RelayCommand<ISDocumentViewModel>(
                ExecuteFileCloseCommand,
                (vm) => vm != null && vm.RequestCloseDocumentCommand.CanExecute(null));
            FileCloseAllCommand = new RelayCommand(
                ExecuteFileCloseAllCommand,
                () => _sDocumentsManager.CloseAllDocumentsCommand.CanExecute(null));
            FileCloseOthersCommand = new RelayCommand<ISDocumentViewModel>(
                ExecuteFileCloseOthersCommand,
                (vm) => vm != null && _sDocumentsManager.CloseAllButCommand.CanExecute(null));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets SDocuments controller.
        /// </summary>
        public SDocumentsManager SDocumentsManagerInstance => _sDocumentsManager;

        #endregion Properties

        #region Commands

        /// <summary>
        /// Gets a command responsible for creating a new section (document) window.
        /// </summary>
        public RelayCommand FileNewCommand { get; private set; }

        /// <summary>
        /// Gets a command responsible for closing a currently opened document.
        /// </summary>
        public RelayCommand<ISDocumentViewModel> FileCloseCommand { get; private set; }

        /// <summary>
        /// Gets a command responsible for closing a all opened documents.
        /// </summary>
        public RelayCommand FileCloseAllCommand { get; private set; }

        /// <summary>
        /// Gets a command responsible for closing a all opened documents, but the selected one.
        /// </summary>
        public RelayCommand<ISDocumentViewModel> FileCloseOthersCommand { get; private set; }

        #endregion Commands

        #region Commands Logic

        /// <summary>
        /// Provides derived classes an opportunity to handle <see cref="RelayCommand.CanExecute(object?)"/>
        /// changes in <see cref="FileCloseAllCommand"/> command.
        /// </summary>
        /// <param name="sender">A reference to the object that raised the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void FileCloseAllCommand_CanExecuteChanged(object sender, EventArgs e)
        {
            FileCloseAllCommand.NotifyCanExecuteChanged();
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle <see cref="RelayCommand.CanExecute(object?)"/>
        /// changes in <see cref="FileCloseOthersCommand"/> command.
        /// </summary>
        /// <param name="sender">A reference to the object that raised the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void FileCloseOthersCommand_CanExecuteChanged(object sender, EventArgs e)
        {
            FileCloseOthersCommand.NotifyCanExecuteChanged();
        }

        // Implements FileNewCommand logic.
        private void ExecuteFileNewCommand()
        {
            WindowService.ShowModalWindow<NewSectionViewModel>();
        }

        // Implements FileCloseCommand logic.
        private void ExecuteFileCloseCommand(ISDocumentViewModel doc)
        {
            doc.RequestCloseDocumentCommand.Execute(null);
        }

        // Implements FileCloseAllCommand logic.
        private void ExecuteFileCloseAllCommand()
        {
            _sDocumentsManager.CloseAllDocumentsCommand.Execute(null);
        }

        // Implements FileCloseOthersCommand logic.
        private void ExecuteFileCloseOthersCommand(ISDocumentViewModel doc)
        {
            _sDocumentsManager.CloseAllButCommand.Execute(null);
        }

        #endregion Commands Logic

        #region Methods

        #endregion Methods
    }
}
