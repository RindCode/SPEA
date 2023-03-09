// ==================================================================================================
// <copyright file="AddGeometryViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels
{
    using System;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using SPEA.App.Commands;
    using SPEA.App.Utils.Services;
    using SPEA.App.ViewModels.SDocument;
    using SPEA.App.ViewModels.Windows;

    /// <summary>
    /// Represents a view model for "Add Geometry" pane.
    /// </summary>
    public class AddGeometryViewModel : ObservableObject
    {
        #region Fields

        private readonly CommandsManager _commandsManager;
        private readonly SDocumentsManagerViewModel _sDocumentsManager;
        private readonly string _addPrimitiveRectCmd = "AddPrimitiveRect";

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddGeometryViewModel"/> class.
        /// </summary>
        /// <param name="commandsManager">A reference to <see cref="CommandsManager.CommandsManager"/> instance.</param>
        /// <param name="sDocumentsManager">A reference to <see cref="SDocumentsManagerViewModel"/> instance.</param>
        public AddGeometryViewModel(
            CommandsManager commandsManager,
            SDocumentsManagerViewModel sDocumentsManager)
        {
            _commandsManager = commandsManager ?? throw new ArgumentNullException(nameof(commandsManager));
            _sDocumentsManager = sDocumentsManager ?? throw new ArgumentNullException(nameof(sDocumentsManager));

            CommandsManager.RegisterCommand(_addPrimitiveRectCmd, new RelayCommand(() => WindowService.ShowModalWindow<AddPrimitiveRectViewModel>(), () => SDocumentsManager.HasItems));

            SDocumentsManager.SDocumentsCollection.CollectionChanged += SDocumentsCollection_CollectionChanged;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a commands manager reference.
        /// </summary>
        public CommandsManager CommandsManager => _commandsManager;

        /// <summary>
        /// Gets a documents manager reference.
        /// </summary>
        public SDocumentsManagerViewModel SDocumentsManager => _sDocumentsManager;

        #endregion Properties

        #region Commands Logic

        // Must be called whenever a command's CanExecute state is changed.
        private void InvalidateCommandsCanExecute()
        {
            var cmd = CommandsManager[_addPrimitiveRectCmd]?.Command as RelayCommand;
            cmd?.NotifyCanExecuteChanged();
        }

        // Is invoked whenever the SDocuments collection is changed.
        private void SDocumentsCollection_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            InvalidateCommandsCanExecute();
        }

        #endregion Commands Logic
    }
}
