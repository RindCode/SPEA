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
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using SPEA.App.Commands;
    using SPEA.App.Controllers;
    using SPEA.App.ViewModels.Interfaces;
    using SPEA.App.ViewModels.SElements;
    using SPEA.Core.CrossSection;

    /// <summary>
    /// A View Model for <see cref="CrossSectionBase"/> model.
    /// Represents a single document of a build-up cross-section and all data stored within.
    /// </summary>
    public class SDocumentViewModel : ObservableObject, ISDocumentViewModel
    {
        #region Fields

        private readonly CommandsManager _commandsManager;
        private readonly SDocumentsManager _sDocumentsManager;
        private readonly string _requestCloseDocumentCmd = "RequestCloseDocument";
        private readonly string _requestSaveDocumenteCmd = "RequestSaveDocument";
        private CrossSectionBase _model;
        private bool _isSaveRequired = false;
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

            // Set fields directly to bypass data changed events.
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _displayName = model.Name ?? throw new ArgumentNullException(nameof(model.Name));

            // Give commands their unique names related to underlying model ID.
            _requestCloseDocumentCmd += $"_{_model.Id}";
            _requestSaveDocumenteCmd += $"_{_model.Id}";

            CommandsManager.RegisterCommand(_requestCloseDocumentCmd, new RelayCommand(ExecuteRequestCloseDocument));
            CommandsManager.RegisterCommand(_requestSaveDocumenteCmd, new RelayCommand(ExecuteRequestSaveDocument));
        }

        #endregion Constructors

        #region Destructor

        /// <summary>
        /// Finalizes an instance of the <see cref="SDocumentViewModel"/> class.
        /// </summary>
        ~SDocumentViewModel()
        {
            CommandsManager.UnregisterCommand(_requestCloseDocumentCmd);
            CommandsManager.UnregisterCommand(_requestSaveDocumenteCmd);
            Model.Dispose();
        }

        #endregion Destructor

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

        /// <inheritdoc/>
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
        public ObservableCollection<SElementViewModelBase> SElements { get; }

        /// <inheritdoc/>
        public string Name
        {
            get => _model.Name;
            set
            {
                SetProperty(_model.Name, value, _model, (model, name) => model.Name = name);
                ////OnDataChanged(this, EventArgs.Empty);
            }
        }

        /// <inheritdoc/>
        public string DisplayName
        {
            get => _displayName;
            private set
            {
                SetProperty(ref _displayName, value);
            }
        }

        /// <inheritdoc/>
        public bool IsSaveRequired
        {
            get => _isSaveRequired;
            private set
            {
                SetProperty(ref _isSaveRequired, value);
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
        public RelayCommand RequestCloseDocumentCommand => (RelayCommand)CommandsManager.GetCommand(_requestCloseDocumentCmd);

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

            IsSaveRequired = false;
            DisplayName = Name;
        }

        #endregion Commands Logic

        #region Methods

        ////private void SDocumentViewModel_DataChanged(object sender, PropertyChangedEventArgs e)
        ////{
        ////    IsSaveRequired = true;
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

        ////    IsSaveRequired = true;
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

        ////    IsSaveRequired = false;
        ////    DisplayName = Name;
        ////}

    #endregion Methods
}
}
