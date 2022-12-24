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
    using SPEA.App.Controllers;
    using SPEA.App.ViewModels.Interfaces;
    using SPEA.App.ViewModels.SElements;
    using SPEA.Core.CrossSection;
    using SPEA.Core.Materials;

    /// <summary>
    /// A View Model for <see cref="CrossSectionBase"/> model.
    /// Represents a single document of a build-up cross-section and all data stored within.
    /// </summary>
    public class SDocumentViewModel : ObservableObject, ISDocumentViewModel
    {
        #region Fields

        // Controller for SDocuments.
        private SDocumentsManager _sDocumentsManager;

        // Holds the reference to the actual cross-section model.
        private CrossSectionBase _model;

        // Indicates if changes were made and local save is required.
        private bool _isSaveRequired = false;

        // Holds the display name of a cross-section. It can differ from the actual stored within the model.
        private string _displayName;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SDocumentViewModel"/> class.
        /// </summary>
        /// <param name="sDocumentsManager">A reference to <see cref="SDocumentsManager"/> instance.</param>
        /// <param name="model">A reference to the model instance.</param>
        public SDocumentViewModel(
            SDocumentsManager sDocumentsManager,
            CrossSectionBase model)
        {
            _sDocumentsManager = sDocumentsManager ?? throw new ArgumentNullException(nameof(sDocumentsManager));

            // Set fields directly to bypass data changed events.
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _displayName = model.Name ?? throw new ArgumentNullException(nameof(model.Name));

            RequestCloseDocumentCommand = new RelayCommand(ExecuteRequestCloseDocument);
            RequestSaveDocumentCommand = new RelayCommand(ExecuteRequestSaveDocument);
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Occurs whenever the document is changed.
        /// </summary>
        public event EventHandler<EventArgs> DataChanged;

        /// <summary>
        /// Occurs whenever the document is saved.
        /// </summary>
        public event EventHandler<EventArgs> DataSaved;

        #endregion Events

        #region Properties

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

        /////// <summary>
        /////// Gets or sets a collection of cross-section materials.
        /////// </summary>
        ////public ObservableCollection<MaterialBase> MaterialsCollection { get; set; } = new ObservableCollection<MaterialBase>();

        /// <inheritdoc/>
        public string Name
        {
            get => _model.Name;
            set
            {
                SetProperty(_model.Name, value, _model, (model, name) => model.Name = name);
                OnDataChanged(this, EventArgs.Empty);
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

        /// <inheritdoc/>
        public RelayCommand RequestCloseDocumentCommand { get; private set; }

        /// <inheritdoc/>
        public RelayCommand RequestSaveDocumentCommand { get; private set; }

        #endregion Commands

        #region Commands Logic

        /// <summary>
        /// Implements <see cref="RequestCloseDocumentCommand"/> command logic.
        /// </summary>
        private void ExecuteRequestCloseDocument()
        {
            _sDocumentsManager.CloseDocumentCommand.Execute(this);
        }

        /// <summary>
        /// Implements <see cref="RequestSaveDocumentCommand"/> command logic.
        /// </summary>
        private void ExecuteRequestSaveDocument()
        {
            // TODO: uncomment and implement
            _ = _sDocumentsManager.SaveDocumentWithDialog(this, string.Empty);
            IsSaveRequired = false;
            DisplayName = Name;
        }

        #endregion Commands Logic

        #region Methods

        /// <summary>
        /// Raises <see cref="DataChanged"/> event.
        /// </summary>
        /// <param name="sender">A reference to the object that raised the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void OnDataChanged(object sender, EventArgs e)
        {
            var handler = DataChanged;
            handler?.Invoke(this, EventArgs.Empty);

            IsSaveRequired = true;
            DisplayName = $"{Name}*";
        }

        /// <summary>
        /// Raises <see cref="DataSaved"/> event.
        /// </summary>
        /// <param name="sender">A reference to the object that raised the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void OnDataSaved(object sender, EventArgs e)
        {
            var handler = DataSaved;
            handler?.Invoke(this, EventArgs.Empty);

            IsSaveRequired = false;
            DisplayName = Name;
        }

        #endregion Methods
    }
}
