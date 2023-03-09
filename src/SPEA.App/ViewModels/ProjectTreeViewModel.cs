// ==================================================================================================
// <copyright file="ProjectTreeViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using CommunityToolkit.Mvvm.ComponentModel;
    using SPEA.App.ViewModels.SDocument;

    /// <summary>
    /// Represents a view model used by Project Tree pane.
    /// </summary>
    public class ProjectTreeViewModel : ObservableObject
    {
        #region Fields

        // Controller for SDocuments.
        private readonly SDocumentsManagerViewModel _sDocumentsManager;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectTreeViewModel"/> class.
        /// </summary>
        /// <param name="sDocumentsManager">A reference to <see cref="SDocumentsManagerViewModel"/> instance.</param>
        public ProjectTreeViewModel(SDocumentsManagerViewModel sDocumentsManager)
        {
            _sDocumentsManager = sDocumentsManager ?? throw new ArgumentNullException(nameof(sDocumentsManager));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a collection of <see cref="Interfaces.SDocumentViewModel"/> documents.
        /// </summary>
        public ObservableCollection<SDocumentViewModel> CrossSectionsCollection => _sDocumentsManager.SDocumentsCollection;

        #endregion Properties
    }
}
