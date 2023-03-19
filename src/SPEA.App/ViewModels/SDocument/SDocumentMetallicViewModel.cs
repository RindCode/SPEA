// ==================================================================================================
// <copyright file="SDocumentMetallicViewModel.cs" company="Dmitry Poberezhnyy">
// Copyright (c) Dmitry Poberezhnyy. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ==================================================================================================

namespace SPEA.App.ViewModels.SDocument
{
    using CommunityToolkit.Mvvm.Messaging;
    using SPEA.App.Commands;
    using SPEA.Core.CrossSections;

    /// <summary>
    /// Represents a view model for <see cref="MetallicCrossSection"/> model type.
    /// </summary>
    public class SDocumentMetallicViewModel : SDocumentViewModel
    {
        #region Fields

        private bool _disposed;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SDocumentMetallicViewModel"/> class.
        /// </summary>
        /// <param name="messenger">A reference to <see cref="IMessenger"/> instance.</param>
        /// <param name="commandsManager">A reference to <see cref="CommandsManager.CommandsManager"/> instance.</param>
        /// <param name="sDocumentsManager">A reference to <see cref="SDocumentsManagerViewModel"/> instance.</param>
        /// <param name="model">A reference to the model instance.</param>
        public SDocumentMetallicViewModel(
            IMessenger messenger,
            CommandsManager commandsManager,
            SDocumentsManagerViewModel sDocumentsManager,
            MetallicCrossSection model)
            : base(messenger, commandsManager, sDocumentsManager, model)
        {
            // Blank.
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
                }

                // Free unmanaged resources (unmanaged objects) and override finalizer
                // Set large fields to null
                _disposed = true;
            }

            base.Dispose(disposing);
        }

        #endregion IDisposable
    }
}
