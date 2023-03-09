// ==================================================================================================
// <copyright file="NewSectionViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels.Windows
{
    using System;
    using System.Collections.Generic;
    using CommunityToolkit.Mvvm.Input;
    using SPEA.App.Commands;
    using SPEA.App.Utils.Helpers;
    using SPEA.App.ViewModels.SDocument;
    using SPEA.Core.CrossSection;

    /// <summary>
    /// A view model used by "create new section" window.
    /// </summary>
    public class NewSectionViewModel : WindowBaseViewModel
    {
        #region Fields

        private readonly SDocumentsManagerViewModel _sDocumentsManager;
        private readonly string _createNewDocumentCmd = "NewSectionWindow.Create";
        private bool _disposed;
        private string _sectionName = string.Empty;
        private string _selectedSectionDefinition = string.Empty;
        private List<string> _sectionDefinitions = new List<string>()
        {
            ResourcesHelper.GetApplicationResource<string>("S.NewSectionWindow.CrossSectionDefinition_Metallic"),
        };

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NewSectionViewModel"/> class.
        /// </summary>
        /// <param name="commandsManager">A reference to <see cref="CommandsManager"/> instance.</param>
        /// <param name="sDocumentsManager">A reference to <see cref="SDocumentsManagerViewModel"/> instance.</param>
        public NewSectionViewModel(
            CommandsManager commandsManager,
            SDocumentsManagerViewModel sDocumentsManager)
            : base(commandsManager)
        {
            _sDocumentsManager = sDocumentsManager ?? throw new ArgumentNullException(nameof(sDocumentsManager));

            CommandsManager.RegisterCommand(_createNewDocumentCmd, new RelayCommand(CreateNewDocument));
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
                    CommandsManager.UnregisterCommand(_createNewDocumentCmd);
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
        /// Gets or sets the cross-section name.
        /// </summary>
        public string SectionName
        {
            get => _sectionName;
            set
            {
                SetProperty(ref _sectionName, value);
            }
        }

        /// <summary>
        /// Gets the cross-section available definitions.
        /// </summary>
        public List<string> SectionDefinitions
        {
            get => _sectionDefinitions;
            private set
            {
                SetProperty(ref _sectionDefinitions, value);
            }
        }

        /// <summary>
        /// Gets or sets the selected cross-section difinition.
        /// </summary>
        public string SelectedSectionDefinition
        {
            get => _selectedSectionDefinition;
            set
            {
                SetProperty(ref _selectedSectionDefinition, value);
            }
        }

        #endregion Properties

        #region Commands Logic

        /// <summary>
        /// Creates a new <see cref="SDocumentViewModel"/> instance and adds it into
        /// a collection of opened documents.
        /// </summary>
        private void CreateNewDocument()
        {
            var definition = ResourcesHelper.GetApplicationResource<string>("S.NewSectionWindow.CrossSectionDefinition_Metallic");

            if (_selectedSectionDefinition == definition)
            {
                var cs = CrossSection.Create<MetallicCrossSection>(SectionName);
                var vm = new SDocumentMetallicViewModel(CommandsManager, _sDocumentsManager, cs);
                _sDocumentsManager.AddDocument(vm);
            }
            else
            {
                var exMsg = ResourcesHelper.GetApplicationResource<string>("S.NewSectionWindow.Exception.UnknowCrossSectionDefinition.Message");
                throw new InvalidOperationException(exMsg);
            }
        }

        #endregion Commands Logic
    }
}
