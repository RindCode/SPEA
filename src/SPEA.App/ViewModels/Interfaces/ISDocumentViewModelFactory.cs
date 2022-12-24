// ==================================================================================================
// <copyright file="ISDocumentViewModelFactory.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels.Interfaces
{
    using SPEA.Core.CrossSection;

    /// <summary>
    /// Provides a contract for a factory of <see cref="ISDocumentViewModel"/> instances.
    /// </summary>
    public interface ISDocumentViewModelFactory
    {
        #region Properties

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates a new instance of <see cref="ISDocumentViewModel"/>.
        /// </summary>
        /// <returns><see cref="ISDocumentViewModel"/> new object.</returns>
        /// <param name="model">Model instance to be used inside the view model.</param>
        /// <returns>View model instance.</returns>
        public ISDocumentViewModel Create(CrossSectionBase model);

        #endregion Methods
    }
}
