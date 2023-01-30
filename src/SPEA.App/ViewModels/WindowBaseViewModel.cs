// ==================================================================================================
// <copyright file="WindowBaseViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels
{
    using System;
    using System.Windows;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using SPEA.App.Commands;

    /// <summary>
    /// A base view model used by all application windows.
    /// Provides basic commands such as minimize, maximize, restore down and close.
    /// </summary>
    public class WindowBaseViewModel : ObservableObject
    {
        #region Fields

        private readonly CommandsManager _commandsManager;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowBaseViewModel"/> class.
        /// </summary>
        /// <param name="commandsManager">A reference to <see cref="Commands.CommandsManager"/> instance.</param>
        public WindowBaseViewModel(
            CommandsManager commandsManager)
        {
            _commandsManager = commandsManager ?? throw new ArgumentNullException(nameof(commandsManager));

            _commandsManager.RegisterCommand(
                "MinimizeWindow",
                new RelayCommand<Window>(ExecuteMinimizeWindow),
                new CommandMetadata(CommandsMetadataOptions.None));
            _commandsManager.RegisterCommand(
                "MaximizeWindow",
                new RelayCommand<Window>(ExecuteMaximizeWindow),
                new CommandMetadata(CommandsMetadataOptions.None));
            _commandsManager.RegisterCommand(
                "RestoreWindow",
                new RelayCommand<Window>(ExecuteRestoreWindow),
                new CommandMetadata(CommandsMetadataOptions.None));
            _commandsManager.RegisterCommand(
                "CloseWindow",
                new RelayCommand<Window>(ExecuteCloseWindow),
                new CommandMetadata(CommandsMetadataOptions.None));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a commands manager reference.
        /// </summary>
        public CommandsManager CommandsManager => _commandsManager;

        #endregion Properties

        #region Commands Logic

        // Minimize window command logic.
        private void ExecuteMinimizeWindow(Window window)
        {
            SystemCommands.MinimizeWindow(window);
        }

        // Maximize window command logic.
        private void ExecuteMaximizeWindow(Window window)
        {
            SystemCommands.MaximizeWindow(window);
        }

        // Restore down window command logic.
        private void ExecuteRestoreWindow(Window window)
        {
            SystemCommands.RestoreWindow(window);
        }

        // Close window command logic.
        private void ExecuteCloseWindow(Window window)
        {
            SystemCommands.CloseWindow(window);
        }

        #endregion Commands Logic
    }
}
