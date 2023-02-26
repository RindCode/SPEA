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
    using SPEA.App.Commands;
    using SPEA.App.Utils.Services;
    using SPEA.App.ViewModels.Windows;

    /// <summary>
    /// A view model used by application main menu.
    /// </summary>
    public class MainMenuViewModel : ObservableObject
    {
        #region Fields

        private readonly CommandsManager _commandsManager;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenuViewModel"/> class.
        /// </summary>
        /// <param name="owner">A reference to a parent view model.</param>
        /// <param name="commandsManager">A reference to <see cref="Commands.CommandsManager"/> instance.</param>
        public MainMenuViewModel(
            CommandsManager commandsManager)
        {
            _commandsManager = commandsManager ?? throw new ArgumentNullException(nameof(commandsManager));

            CommandsManager.RegisterCommand("FileNew", new RelayCommand(() => WindowService.ShowModalWindow<NewSectionViewModel>()));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a commands manager reference.
        /// </summary>
        public CommandsManager CommandsManager => _commandsManager;

        #endregion Properties
    }
}
