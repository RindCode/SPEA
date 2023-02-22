// ==================================================================================================
// <copyright file="SDocumentViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Shapes;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using SPEA.App.Commands;
    using SPEA.App.Controllers;
    using SPEA.App.ViewModels.SElements;
    using SPEA.Core.CrossSection;
    using SPEA.Geometry.Core;
    using SPEA.Geometry.Primitives;

    /// <summary>
    /// A base view model class for <see cref="CrossSectionBase"/> model.
    /// Represents a single document of a build-up cross-section and all data stored within.
    /// </summary>
    public abstract class SDocumentViewModel : ObservableObject, IDisposable
    {
        #region Fields

        private readonly CommandsManager _commandsManager;
        private readonly SDocumentsManager _sDocumentsManager;
        private readonly string _requestCloseDocumentCmd = "RequestCloseDocument";
        private readonly string _requestSaveDocumenteCmd = "RequestSaveDocument";
        private readonly ObservableCollection<SElementViewModelBase> _sElements;
        private bool _disposed;
        private CrossSectionBase _model;
        private bool _isDirty = false;
        private string _displayName = string.Empty;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SDocumentViewModel"/> class.
        /// </summary>
        /// <param name="commandsManager">A reference to <see cref="CommandsManager.CommandsManager"/> instance.</param>
        /// <param name="sDocumentsManager">A reference to <see cref="SDocumentsManager"/> instance.</param>
        /// <param name="model">A reference to the model instance.</param>
        public SDocumentViewModel(
            CommandsManager commandsManager,
            SDocumentsManager sDocumentsManager,
            CrossSectionBase model)
        {
            _commandsManager = commandsManager ?? throw new ArgumentNullException(nameof(commandsManager));
            _sDocumentsManager = sDocumentsManager ?? throw new ArgumentNullException(nameof(sDocumentsManager));

            _model = model ?? throw new ArgumentNullException(nameof(model));
            _displayName = model.Name ?? throw new ArgumentNullException(nameof(model.Name));
            _sElements = new ObservableCollection<SElementViewModelBase>();

            // Give commands their unique names related to this model ID.
            _requestCloseDocumentCmd += $"_{_model.Guid}";
            _requestSaveDocumenteCmd += $"_{_model.Guid}";

            CommandsManager.RegisterCommand(_requestCloseDocumentCmd, new RelayCommand(ExecuteRequestCloseDocument));
            CommandsManager.RegisterCommand(_requestSaveDocumenteCmd, new RelayCommand(ExecuteRequestSaveDocument));

            var vm = new SRectViewModel();
            vm.Width = 100;
            vm.Height = 50;
            SElements.Add(vm);
        }

        #endregion Constructors

        #region Destructor

        /*
        // Override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~SDocumentViewModel()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }
        */

        #endregion Destructor

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
                    CommandsManager.UnregisterCommand(_requestCloseDocumentCmd);
                    CommandsManager.UnregisterCommand(_requestSaveDocumenteCmd);
                    Model?.Dispose();
                }

                // Free unmanaged resources (unmanaged objects) and override finalizer
                // Set large fields to null
                _disposed = true;
            }
        }

        #endregion IDisposable

        #region Events

        /////// <summary>
        /////// Occurs whenever the document is changed.
        /////// </summary>
        ////public event EventHandler<EventArgs> DataChanged;

        /////// <summary>
        /////// Occurs whenever the document is saved.
        /////// </summary>
        ////public event EventHandler<EventArgs> DataSaved;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets a commands manager reference.
        /// </summary>
        public CommandsManager CommandsManager => _commandsManager;

        /// <summary>
        /// Gets a documents manager reference.
        /// </summary>
        public SDocumentsManager SDocumentsManager => _sDocumentsManager;

        /// <summary>
        /// Gets a cross-section model.
        /// </summary>
        public CrossSectionBase Model
        {
            get => _model;
            private set
            {
                SetProperty(ref _model, value);
            }
        }

        /// <summary>
        /// Gets the reference to the collection of cross-section SElements.
        /// </summary>
        public ObservableCollection<SElementViewModelBase> SElements => _sElements;

        /// <summary>
        /// Gets or sets a cross-section name.
        /// </summary>
        public string Name
        {
            get => _model.Name;
            set
            {
                SetProperty(_model.Name, value, _model, (model, name) => model.Name = name);
                ////OnDataChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets a cross-section display name.
        /// </summary>
        public string DisplayName
        {
            get => _displayName;
            private set
            {
                SetProperty(ref _displayName, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the data was modified and needs to be saved.
        /// </summary>
        public bool IsDirty
        {
            get => _isDirty;
            private set
            {
                SetProperty(ref _isDirty, value);
            }
        }

        #endregion Properties

        #region Commands

        /// <summary>
        /// Gets a command which requests the selected document to be closed
        /// and removed from the list of active documents.
        /// </summary>
        /// <remarks>
        /// Gets a command previously registered in <see cref="CommandsManager.CommandsManager"/>.
        /// </remarks>
        public RelayCommand RequestCloseDocumentCommand => CommandsManager.GetCommand(_requestCloseDocumentCmd) as RelayCommand;

        #endregion Commands

        #region Commands Logic

        /////// <summary>
        /////// Implements <see cref="RequestCloseDocumentCommand"/> command logic.
        /////// </summary>
        private void ExecuteRequestCloseDocument()
        {
            _ = _sDocumentsManager.RemoveDocumentWithConfirmation(this);
        }

        /////// <summary>
        /////// Implements <see cref="RequestSaveDocumentCommand"/> command logic.
        /////// </summary>
        private void ExecuteRequestSaveDocument()
        {
            _ = _sDocumentsManager.SaveDocumentWithDialog(this, string.Empty);

            IsDirty = false;
            DisplayName = Name;
        }

        #endregion Commands Logic

        #region Methods

        internal void AddPolygon(SPolygonBase polygon)
        {
            Model.AddPolygon(polygon);
        }

        ////private void SDocumentViewModel_DataChanged(object sender, PropertyChangedEventArgs e)
        ////{
        ////    IsDirty = true;
        ////    DisplayName = $"{Name}*";
        ////}

        /////// <summary>
        /////// Raises <see cref="DataChanged"/> event.
        /////// </summary>
        /////// <param name="sender">A reference to the object that raised the event.</param>
        /////// <param name="e">Event data.</param>
        ////protected virtual void OnDataChanged(object sender, EventArgs e)
        ////{
        ////    var handler = DataChanged;
        ////    handler?.Invoke(this, EventArgs.Empty);

        ////    IsDirty = true;
        ////    DisplayName = $"{Name}*";
        ////}

        /////// <summary>
        /////// Raises <see cref="DataSaved"/> event.
        /////// </summary>
        /////// <param name="sender">A reference to the object that raised the event.</param>
        /////// <param name="e">Event data.</param>
        ////protected virtual void OnDataSaved(object sender, EventArgs e)
        ////{
        ////    var handler = DataSaved;
        ////    handler?.Invoke(this, EventArgs.Empty);

        ////    IsDirty = false;
        ////    DisplayName = Name;
        ////}

        #endregion Methods
    }
}
