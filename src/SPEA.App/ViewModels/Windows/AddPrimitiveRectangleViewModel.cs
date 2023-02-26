// ==================================================================================================
// <copyright file="AddPrimitiveRectangleViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels.Windows
{
    using System;
    using CommunityToolkit.Mvvm.Input;
    using SPEA.App.Commands;
    using SPEA.App.Controllers;
    using SPEA.App.ViewModels.SElements;

    /// <summary>
    /// A view model used by "add new rectangle primitive" window.
    /// </summary>
    public class AddPrimitiveRectangleViewModel : WindowBaseViewModel
    {
        #region Fields

        private readonly SDocumentsManager _sDocumentsManager;
        private readonly string _addPrimitiveRectangleCmd = "AddPrimitiveRectangleWindow.Add";
        private bool _disposed;
        private double _width;
        private double _height;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPrimitiveRectangleViewModel"/> class.
        /// </summary>
        /// <param name="commandsManager">A reference to <see cref="CommandsManager"/> instance.</param>
        /// <param name="sDocumentsManager">A reference to <see cref="SDocumentsManager"/> instance.</param>
        public AddPrimitiveRectangleViewModel(
            CommandsManager commandsManager,
            SDocumentsManager sDocumentsManager)
            : base(commandsManager)
        {
            _sDocumentsManager = sDocumentsManager ?? throw new ArgumentNullException(nameof(sDocumentsManager));

            CommandsManager.RegisterCommand(_addPrimitiveRectangleCmd, new RelayCommand(AddPrimitiveRectangle));
        }

        #endregion Constructors

        #region IDisposable

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects)
                    CommandsManager.UnregisterCommand(_addPrimitiveRectangleCmd);
                }

                // Free unmanaged resources (unmanaged objects) and override finalizer
                // Set large fields to null
                _disposed = true;
            }

            base.Dispose(disposing);
        }

        #endregion IDisposable

        #region Properties

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
        private void AddPrimitiveRectangle()
        {
            var vm = new SRectViewModel()
            {
                W = Width,
                H = Height,
            };

            _sDocumentsManager.SelectedDocument.AddSElement(vm);
        }

        #endregion Commands Logic
    }
}
