// ==================================================================================================
// <copyright file="AddPrimitiveRectangleViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels.SElements
{
    using System;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using SPEA.App.Commands;
    using SPEA.App.Controllers;
    using SPEA.App.Utils.Helpers;
    using SPEA.Core.CrossSection;

    /// <summary>
    /// A view model used by "add new rectangle primitive" window.
    /// </summary>
    public class AddPrimitiveRectangleViewModel : ObservableObject
    {
        #region Fields

        private readonly CommandsManager _commandsManager;
        private readonly string _addPrimitiveRectangleCmd = "AddPrimitiveRectangleWindow.Add";
        private double _width;
        private double _height;
        private bool _isWidthValid;
        private bool _isHeightValid;
        private bool _isInputValid;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPrimitiveRectangleViewModel"/> class.
        /// </summary>
        /// <param name="commandsManager">A reference to <see cref="CommandsManager"/> instance.</param>
        public AddPrimitiveRectangleViewModel(
            CommandsManager commandsManager)
        {
            _commandsManager = commandsManager ?? throw new ArgumentNullException(nameof(commandsManager));

            CommandsManager.RegisterCommand(_addPrimitiveRectangleCmd, new RelayCommand(AddRectanglePrimitive));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a commands manager reference.
        /// </summary>
        public CommandsManager CommandsManager => _commandsManager;

        /// <summary>
        /// Gets or sets a width values used to create a rectangle primitive.
        /// </summary>
        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        /// <summary>
        /// Gets or sets a width values used to create a rectangle primitive.
        /// </summary>
        public double Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        #endregion Properties

        #region Commands Logic

        /// <summary>
        /// Creates a new rectangle primitive and adds it into a collection
        /// of cross-section geometry elements.
        /// </summary>
        private void AddRectanglePrimitive()
        {
            var vm = new SRectViewModel();
            vm.Width = Width;
            vm.Height = Height;
        }

        #endregion Commands Logic
    }
}
