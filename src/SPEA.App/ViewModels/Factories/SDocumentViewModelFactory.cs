// ==================================================================================================
// <copyright file="SDocumentViewModelFactory.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels.Factories
{
    using SPEA.App.Controllers;
    using SPEA.App.ViewModels.Interfaces;
    using SPEA.Core.CrossSection;

    /// <summary>
    /// Represents a factory than implements <see cref="ISDocumentViewModelFactory"/> interface
    /// and creates <see cref="ISDocumentViewModel"/> objects.
    /// </summary>
    public class SDocumentViewModelFactory : ISDocumentViewModelFactory
    {
        #region Fields

        private SDocumentsManager _sDocumentsManager;

        #endregion Fields

        #region Constructors


        /// <summary>
        /// Initializes a new instance of the <see cref="SDocumentViewModelFactory"/> class.
        /// </summary>
        /// <param name="sDocumentsManager">A reference to <see cref="SDocumentsManager"/> instance.</param>
        public SDocumentViewModelFactory(SDocumentsManager sDocumentsManager)
        {
            _sDocumentsManager = sDocumentsManager;
        }

        #endregion Constructors

        #region Methods

        /// <inheritdoc/>
        public ISDocumentViewModel Create(CrossSectionBase model)
        {
            return new SDocumentViewModel(_sDocumentsManager, model);
        }

        #endregion Methods
    }
}
