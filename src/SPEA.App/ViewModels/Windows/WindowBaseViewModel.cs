// ==================================================================================================
// <copyright file="WindowBaseViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels.Windows
{
    using System;
    using System.Windows;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using SPEA.App.Commands;
    using SPEA.App.ViewModels.Interfaces;

    /// <summary>
    /// A base view model used by all application windows.
    /// Provides basic commands such as minimize, maximize, restore down and close.
    /// </summary>
    public class WindowBaseViewModel : ObservableObject, IDisposableViewModel
    {
        #region Fields

        private readonly string _minimizeWindowCmd = "MinimizeWindow";
        private readonly string _maximizeWindowCmd = "MaximizeWindow";
        private readonly string _restoreWindowCmd = "RestoreWindow";
        private readonly string _closeWindowCmd = "CloseWindow";
        private readonly CommandsManager _commandsManager;
        private bool _disposed;

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

            _commandsManager.RegisterCommand(_minimizeWindowCmd, new RelayCommand<Window>(ExecuteMinimizeWindow));
            _commandsManager.RegisterCommand(_maximizeWindowCmd, new RelayCommand<Window>(ExecuteMaximizeWindow));
            _commandsManager.RegisterCommand(_restoreWindowCmd, new RelayCommand<Window>(ExecuteRestoreWindow));
            _commandsManager.RegisterCommand(_closeWindowCmd, new RelayCommand<Window>(ExecuteCloseWindow));
        }

        #endregion Constructors

        #region IDisposable

        /// <inheritdoc/>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implements Dispose pattern.
        /// </summary>
        /// <param name="disposing">Designates whether the method was called from Dispose() or not.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects)
                    CommandsManager.UnregisterCommand(_minimizeWindowCmd);
                    CommandsManager.UnregisterCommand(_maximizeWindowCmd);
                    CommandsManager.UnregisterCommand(_restoreWindowCmd);
                    CommandsManager.UnregisterCommand(_closeWindowCmd);
                }

                // Free unmanaged resources (unmanaged objects) and override finalizer
                // Set large fields to null
                _disposed = true;
            }
        }

        #endregion IDisposable

        #region Properties

        /// <summary>
        /// Gets a commands manager reference.
        /// </summary>
        public CommandsManager CommandsManager => _commandsManager;

        #endregion Properties

        #region Commands

        /// <inheritdoc/>
        public RelayCommand DisposeViewModelCommand => new RelayCommand(Dispose);

        #endregion Commands

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
