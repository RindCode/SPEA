// ==================================================================================================
// <copyright file="WindowBaseViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels
{
    using System.Windows;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;

    /// <summary>
    /// A base view model used by all application windows.
    /// Provides basic commands such as minimize, maximize, restore down and close.
    /// </summary>
    public class WindowBaseViewModel : ObservableObject
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowBaseViewModel"/> class.
        /// </summary>
        public WindowBaseViewModel()
        {
            // Commands.
            MinimizeWindowCommand = new RelayCommand<Window>(ExecuteMinimizeWindow);
            MaximizeWindowCommand = new RelayCommand<Window>(ExecuteMaximizeWindow);
            RestoreWindowCommand = new RelayCommand<Window>(ExecuteRestoreWindow);
            CloseWindowCommand = new RelayCommand<Window>(ExecuteCloseWindow);
        }

        #endregion Constructors

        #region Properties

        #endregion Properties

        #region Commands

        /// <summary>
        /// Gets a command which minimizes the window.
        /// </summary>
        public RelayCommand<Window> MinimizeWindowCommand { get; private set; }

        /// <summary>
        /// Gets a command which maximizes the window.
        /// </summary>
        public RelayCommand<Window> MaximizeWindowCommand { get; private set; }

        /// <summary>
        /// Gets a command which restores the window.
        /// </summary>
        public RelayCommand<Window> RestoreWindowCommand { get; private set; }

        /// <summary>
        /// Gets a command which closes the window.
        /// </summary>
        public RelayCommand<Window> CloseWindowCommand { get; private set; }

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

        #region Methods

        #endregion Methods
    }
}
