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
    using SPEA.App.Controllers;
    using SPEA.App.ViewModels.Interfaces;

    /// <summary>
    /// A view model used by Project Tree user control.
    /// </summary>
    public class ProjectTreeViewModel : ObservableObject
    {
        #region Fields

        // Controller for SDocuments.
        private SDocumentsManager _sDocumentsManager;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectTreeViewModel"/> class.
        /// </summary>
        /// <param name="sDocumentsManager">A reference to <see cref="SDocumentsManager"/> instance.</param>
        public ProjectTreeViewModel(SDocumentsManager sDocumentsManager)
        {
            _sDocumentsManager = sDocumentsManager ?? throw new ArgumentNullException(nameof(sDocumentsManager));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a collection of <see cref="ISDocumentViewModel"/> documents.
        /// </summary>
        public ObservableCollection<ISDocumentViewModel> CrossSectionsCollection => _sDocumentsManager.SDocumentsCollection;

        #endregion Properties
    }
}
