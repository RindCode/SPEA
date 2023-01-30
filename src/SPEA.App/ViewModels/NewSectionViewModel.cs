// ==================================================================================================
// <copyright file="NewSectionViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels
{
    using System;
    using System.Collections.Generic;
    using CommunityToolkit.Mvvm.Input;
    using SPEA.App.Commands;
    using SPEA.App.Controllers;
    using SPEA.App.Utils.Helpers;
    using SPEA.App.ViewModels.Interfaces;
    using SPEA.Core.CrossSection;

    /// <summary>
    /// A view model used by "create new section" window.
    /// </summary>
    public class NewSectionViewModel : WindowBaseViewModel
    {
        #region Fields

        private readonly SDocumentsManager _sDocumentsManager;
        private readonly ISDocumentViewModelFactory _sDocumentViewModelFactory;
        private readonly string _createNewDocumentCmd = "CreateNewDocument";
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
        /// <param name="sDocumentsManager">A reference to <see cref="SDocumentsManager"/> instance.</param>
        /// <param name="sDocumentViewModelFactory">A reference to <see cref="ISDocumentViewModelFactory"/> instance.</param>
        public NewSectionViewModel(
            CommandsManager commandsManager,
            SDocumentsManager sDocumentsManager,
            ISDocumentViewModelFactory sDocumentViewModelFactory)
            : base(commandsManager)
        {
            _sDocumentsManager = sDocumentsManager ?? throw new ArgumentNullException(nameof(sDocumentsManager));
            _sDocumentViewModelFactory = sDocumentViewModelFactory ?? throw new ArgumentNullException(nameof(sDocumentViewModelFactory));

            CommandsManager.RegisterCommand(_createNewDocumentCmd, new RelayCommand<string>(CreateNewDocument));
        }

        #endregion Constructors

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
        /// <param name="name">A document name.</param>
        private void CreateNewDocument(string name)
        {
            if (_selectedSectionDefinition == ResourcesHelper.GetApplicationResource<string>("S.NewSectionWindow.CrossSectionDefinition_Metallic"))
            {
                var cs = CrossSection.Create<MetallicCrossSection>(name);
                var vm = _sDocumentViewModelFactory.Create(cs);
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
