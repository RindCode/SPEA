// ==================================================================================================
// <copyright file="ISDocumentViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels.Interfaces
{
    using CommunityToolkit.Mvvm.Input;
    using SPEA.Core.CrossSection;

    /// <summary>
    /// Provides a contract to create SDocument instances.
    /// </summary>
    public interface ISDocumentViewModel
    {
        #region Properties

        /// <summary>
        /// Gets the document model instance.
        /// </summary>
        public CrossSectionBase Model { get; }

        /// <summary>
        /// Gets or sets the section document name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the section document display name.
        /// </summary>
        /// <remarks>
        /// The display name will be either the same as <see cref="Name"/>,
        /// or will end with asterisk indicating there are unsaved changes.
        /// </remarks>
        public string DisplayName { get; }

        /// <summary>
        /// Gets a value indicating whether there are unsaved changes in the document or not.
        /// </summary>
        public bool IsSaveRequired { get; }

        #endregion Properties

        #region Commands

        /// <summary>
        /// Gets a command which requests the selected document to be closed
        /// and removes from the list of active documents.
        /// </summary>
        public RelayCommand RequestCloseDocumentCommand { get; }

        /// <summary>
        /// Gets a command which requests the selected document to be saved.
        /// </summary>
        public RelayCommand RequestSaveDocumentCommand { get; }

        #endregion Commands

        #region Methods

        #endregion Methods
    }
}
